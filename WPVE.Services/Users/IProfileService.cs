using System;
using System.IO;

namespace WPVE.Services.Users
{
    public interface IProfileService
    {
        Task<Profile> GetUserProfileAsync(string id);
        Task<bool> ChangeUserAvatarAsync(string id, string img64base);
        Task<bool> ChangeUserFullNameAsync(string id, string first_name, string last_name);
        Task<List<string>> GetUserRolesAsync(string id);
        Task<bool> ResetUserPassAsync(string id);
        Task<string> GetThemeColorAsync(string id);
        Task<List<Profile>> getUserProfileListAsync();
        Task<bool> DeleteUserAsync(string id);
        Task<bool> HaveProfile(string id);
    }
}

