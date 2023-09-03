using System;
using Microsoft.AspNetCore.Identity;
using WPVE.Data;

namespace WPVE.Services.Users
{
    public class ProfileService :IProfileService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileService(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Profile> GetUserProfileAsync(string id)
        {
            Profile model = new Profile();
            if (string.IsNullOrWhiteSpace(id))
            {
                return model;
            }
            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var profile = await Task.FromResult(_context.Profiles.Find(id));
                model.Username = user.UserName;
                model.Email = user.Email;
                model.Id = user.Id;
                model.Phonenumber = user.PhoneNumber;
                if (profile != null)
                {
                    model.First_Name = profile.FirstName;
                    model.Last_Name = profile.LastName;
                    model.Theme_Color = profile.ThemeColor;
                    model.Avatar = profile.Avatar;
                    model.navigation_size = profile.NavigationSize;
                }
                var role = await GetUserRolesAsync(user.Id);

                if (role.Count > 0)
                {
                    model.Role = role[0];
                }


            }
            return model;
        }

        public async Task<bool> ChangeUserAvatarAsync(string id, string img64base)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var profile = await Task.FromResult(_context.Profiles.Find(user.Id));
                if (profile == null)
                {
                    _context.Profiles.Add(new WPVE.Core.Domain.Users.Profile() { Id = user.Id, FirstName = String.Empty, LastName = String.Empty, ThemeColor = "light", Avatar = img64base, NavigationSize = "normal-navigation" });
                    await _context.SaveChangesAsync();
                    return true;
                }
                profile.Avatar = img64base;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeUserFullNameAsync(string id, string first_name, string last_name)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var profile = await Task.FromResult(_context.Profiles.Find(user.Id));
                if (profile == null)
                {
                    _context.Profiles.Add(new WPVE.Core.Domain.Users.Profile() { Id = user.Id, FirstName = first_name, LastName = last_name, ThemeColor = "light", Avatar = "", NavigationSize = "normal-navigation" });
                }
                else
                {
                    profile.FirstName = first_name;
                    profile.LastName = last_name;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<string>> GetUserRolesAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new List<string>();
            }
            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var roles = await Task.FromResult(_context.UserRoles.Where(x => x.UserId == user.Id).ToList());
                List<string> user_roles = new List<string>();
                if (roles != null)
                {
                    if (roles.Count > 0)
                    {
                        foreach (var item in roles)
                        {
                            var role = _context.Roles.Find(item.RoleId);
                            if (role != null)
                            {
                                user_roles.Add(role.Name);
                            }
                        }
                        return user_roles;
                    }
                }

            }
            return new List<string>();
        }

        public async Task<bool> ResetUserPassAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, code, "qwerty");
                return true;

            }
            return false;
        }

        public async Task<string> GetThemeColorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return "light";
            }
            var profile = await Task.FromResult(_context.Profiles.Find(id));
            if (profile != null)
            {
                return profile.ThemeColor;
            }
            return "light";
        }

        public Task<List<Profile>> getUserProfileListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            var user = await Task.FromResult(_context.Users.Find(id));
            if (user != null)
            {
                var profile = await Task.FromResult(_context.Profiles.Find(user.Id));
                if (profile != null)
                {
                    _context.Profiles.Remove(profile);
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<bool> HaveProfile(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            var profile = await Task.FromResult(_context.Profiles.Find(id));
            if (profile != null)
            {
                return true;
            }
            return false;
        }
    }
}

