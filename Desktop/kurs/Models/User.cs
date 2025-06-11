using Microsoft.AspNetCore.Identity;

namespace YourApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public virtual UserProfile Profile { get; set; }
    }

    public class UserProfile
    {
        public int Id { get; set; }
        public string Sport { get; set; }
        public string SkillLevel { get; set; }
        public string City { get; set; }
        public int Age { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
