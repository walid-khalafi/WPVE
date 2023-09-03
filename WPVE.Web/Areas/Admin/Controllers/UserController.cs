using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WPVE.Data;
using WPVE.Services.Users;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        #endregion

        #region Ctor
        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion

        #region Methods

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadAjaxUsers()
        {
            // server side parameters
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"].ToString();
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"].ToString();

            List<Profile> list = new List<Profile>();

            var users = await Task.FromResult(_context.Users.ToList());

            if (users == null)
            {
                return Json(new { data = list, draw = Request.Form["draw"].ToString(), recordsTotal = 0, recordsFiltered = 0 });
            }

            int recordsTotal = users.Count;

            foreach (var item in users)
            {
                var profile = await _profileService.GetUserProfileAsync(item.Id);
                var roles = await _profileService.GetUserRolesAsync(item.Id);
                if (profile != null)
                {
                    list.Add(new Profile()
                    {
                        Id = profile.Id,
                        Avatar = (!string.IsNullOrWhiteSpace(profile.Avatar) ? profile.Avatar : ""),
                        Email = profile.Email,
                        First_Name = profile.First_Name,
                        Last_Name = profile.Last_Name,
                        Phonenumber = profile.Phonenumber,
                        Theme_Color = profile.Theme_Color,
                        Username = profile.Username,
                        TwoFactorEnabled = item.TwoFactorEnabled,
                        Role = (roles.Count > 0 ? roles[0] : ""),
                    });
                }

            }

            list = list.OrderBy(x => x.Last_Name).ToList();
            if (!string.IsNullOrEmpty(searchValue)) // fiter 
            {
                list = list.Where(x => x.Full_Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            if (sortDirection == "asc")
            {
                switch (sortColumnName)
                {
                    case "First_Name":
                        list = list.OrderBy(x => x.First_Name).ToList();
                        break;
                    case "Last_Name":
                        list = list.OrderBy(x => x.Last_Name).ToList();
                        break;
                    case "Username":
                        list = list.OrderBy(x => x.Username).ToList();
                        break;
                    default:
                        break;
                }

            }
            else
            {
                switch (sortColumnName)
                {
                    case "First_Name":
                        list = list.OrderByDescending(x => x.First_Name).ToList();
                        break;
                    case "Last_Name":
                        list = list.OrderByDescending(x => x.Last_Name).ToList();
                        break;
                    case "Username":
                        list = list.OrderByDescending(x => x.Username).ToList();
                        break;
                    default:
                        break;
                }
            }

            // paging
            list = list.Skip(start).Take(length).ToList();

            int recordsFilteredTotal = list.Count;
            return Json(new { data = list, draw = Request.Form["draw"].ToString(), recordsTotal = recordsTotal, recordsFiltered = recordsTotal });

        }

        [HttpGet]
        public async Task<IActionResult> ResetUserPass(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return StatusCode(404);
            }
            var result = await _profileService.ResetUserPassAsync(id);
            if (result)
            {
                TempData["success_msg"] = "رمز عبور با موفقیت به qwerty بازنشانی شد.";
            }
            else
            {
                TempData["error_msg"] = "خطا در بازنشانی رمز عبور کاربر رخ داده است.";
            }

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<JsonResult> ChangeTheme(string id, string theme)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(theme))
            {
                return Json("false");
            }

            var profile = await Task.FromResult(_context.Profiles.FirstOrDefault(x => x.Id == id));
            if (profile == null)
            {
                return Json("ProfileNotFound");
            }

            profile.ThemeColor = theme;
            await _context.SaveChangesAsync();
            return Json("true");
        }

        [HttpPost]
        public async Task<JsonResult> ChangeNavigation(string id, string size)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(size))
            {
                return Json("false");
            }

            var profile = await Task.FromResult(_context.Profiles.FirstOrDefault(x => x.Id == id));
            if (profile == null)
            {
                return Json("ProfileNotFound");
            }

            profile.NavigationSize = size;
            await _context.SaveChangesAsync();
            return Json("true");
        }


        #endregion
    }
}