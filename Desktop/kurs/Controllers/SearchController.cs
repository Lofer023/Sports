[HttpGet]
public async Task<IActionResult> FindPartners(FindPartnersViewModel model)
{
    var query = _context.Users
        .Include(u => u.Profile)
        .Where(u => u.Profile.IsActive && u.Role == "User");

    if (!string.IsNullOrEmpty(model.Sport))
    {
        query = query.Where(u => u.Profile.Sport == model.Sport);
    }
    if (!string.IsNullOrEmpty(model.City))
    {
        query = query.Where(u => u.Profile.City.Contains(model.City));
    }
    if (!string.IsNullOrEmpty(model.SkillLevel))
    {
        query = query.Where(u => u.Profile.SkillLevel == model.SkillLevel);
    }
    if (model.AgeFrom.HasValue)
    {
        query = query.Where(u => u.Profile.Age >= model.AgeFrom.Value);
    }
    if (model.AgeTo.HasValue)
    {
        query = query.Where(u => u.Profile.Age <= model.AgeTo.Value);
    }

    var partners = await query
        .Select(u => new PartnerInfo
        {
            Id = u.Id,
            FullName = u.FirstName + " " + u.LastName,
            Sport = u.Profile.Sport,
            SkillLevel = u.Profile.SkillLevel,
            City = u.Profile.City,
            Age = u.Profile.Age
        })
        .ToListAsync();

    model.Partners = partners;
    model.SportList = await GetSportSelectListAsync();
    model.SkillLevelList = await GetSkillLevelSelectListAsync();

    return View(model);
}

private async Task<IEnumerable<SelectListItem>> GetSportSelectListAsync()
{
    // Приклад: отримання списку видів спорту із БД або з констант
    return new List<SelectListItem>
    {
        new SelectListItem("Футбол", "Football"),
        new SelectListItem("Теніс", "Tennis"),
        new SelectListItem("Біг", "Running"),
        new SelectListItem("Плавання", "Swimming")
    };
}

private async Task<IEnumerable<SelectListItem>> GetSkillLevelSelectListAsync()
{
    return new List<SelectListItem>
    {
        new SelectListItem("Початківець", "Beginner"),
        new SelectListItem("Середній", "Intermediate"),
        new SelectListItem("Просунутий", "Advanced")
    };
}
