using YourApp.Models;

namespace YourApp.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task UpdateUserProfileAsync(UserProfile profile);
        // Додаткові методи
    }
}
