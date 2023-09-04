using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPVE.Web.Areas.Admin.Models.BlogViewModel;
using WPVE.Data;
using System.Drawing.Imaging;
using System.Net;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _user_id;
        private string _ipAddress;

        #endregion

        #region Ctor
        public BlogController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor) {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _ipAddress = _httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _user_id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        #endregion


        #region Methods
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Admin/Blog/GetPosts")]
        public async Task<IActionResult> GetPostsAsync()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            var posts = await Task.FromResult( _db.BlogPosts.Select(item => new BlogIndexViewModel
            {
                Id = item.Id,
                Title = item.Title,
                CreatedOn = item.CreatedOnUtc,
                StartDateUtc = item.StartDateUtc,
                EndDateUtc = item.EndDateUtc
            }));

            var filteredPosts = FilterPosts(posts, searchValue);
            var sortedPosts = SortPosts(filteredPosts, sortColumnName, sortDirection);
            var pagedPosts = sortedPosts.Skip(start).Take(length);

            int recordsTotal = posts.Count();
            int recordsFilteredTotal = filteredPosts.Count();

            return Json(new
            {
                data = pagedPosts,
                draw = Request.Form["draw"],
                recordsTotal,
                recordsFiltered = recordsFilteredTotal
            });
        }

        private IEnumerable<BlogIndexViewModel> FilterPosts(IEnumerable<BlogIndexViewModel> posts, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                string searchLower = searchValue.ToLower();
                posts = posts.Where(x => x.Title.ToLower().Contains(searchLower));
            }
            return posts;
        }

        private IEnumerable<BlogIndexViewModel> SortPosts(IEnumerable<BlogIndexViewModel> posts, string sortColumnName, string sortDirection)
        {
            switch (sortColumnName)
            {
                case "Title":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.Title) : posts.OrderByDescending(x => x.Title);
                    break;
                case "StartDateUtc":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.StartDateUtc) : posts.OrderByDescending(x => x.StartDateUtc);
                    break;
                case "EndDateUtc":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.EndDateUtc) : posts.OrderByDescending(x => x.EndDateUtc);
                    break;
                case "CreatedOn":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.CreatedOn) : posts.OrderByDescending(x => x.CreatedOn);
                    break;
            }
            return posts;
        }

        /// <summary>
        /// Add or Update Blog Post Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddPostOrUpdate(string? id)
        {
            var categories = await _db.blogCategories.ToListAsync();
            ViewData["BlogPostCategoryId"] = new SelectList(categories, "Id", "Title");
            if (!string.IsNullOrWhiteSpace(id))
            {
                var data = await _db.BlogPosts.FindAsync(id);
                if (data != null)
                {
                    BlogAddOrUpdateViewModel model = new BlogAddOrUpdateViewModel()
                    {
                        Id = id,
                        AllowComments = data.AllowComments,
                        BlogPostCategoryId = data.BlogPostCategoryId,
                        Body = data.Body,
                        BodyOverview = data.BodyOverview,
                        EndDateUtc = PersianDate.Standard.ConvertDate.ToFa(data.EndDateUtc),
                        StartDateUtc = PersianDate.Standard.ConvertDate.ToFa(data.StartDateUtc),
                        IncludeInSitemap = data.IncludeInSitemap,
                        MetaDescription = data.MetaDescription,
                        MetaKeywords = data.MetaKeywords,
                        MetaTitle = data.MetaTitle,
                        SeName = data.Title,
                        Tags = data.Tags,
                        Title = data.Title,
                    };
                    return View(model);
                }
            }
            return View();
        }

        /// <summary>
        /// Add or Update Blog Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddPostOrUpdate(BlogAddOrUpdateViewModel model)
        {
            if (model == null)
            {
                return Json("BadRequest");
            }

            DateTime StartDateUtc = new DateTime(DateTime.Now.Date.Year,DateTime.Now.Date.Month,DateTime.Now.Date.Day);
            DateTime EndDateUtc = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);

            if (!string.IsNullOrWhiteSpace(model.StartDateUtc))
            {
               StartDateUtc = PersianDate.Standard.ConvertDate.ToEn(model.StartDateUtc);
            }

            if (!string.IsNullOrWhiteSpace(model.EndDateUtc))
            {
                EndDateUtc = PersianDate.Standard.ConvertDate.ToEn(model.EndDateUtc);

                if (StartDateUtc > EndDateUtc)
                {
                    return Json("BadStartDateTime");
                }
            }

            if (string.IsNullOrWhiteSpace(model.Id))
            {
                _db.BlogPosts.Add(new Core.Domain.Blogs.BlogPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = model.Title,
                    BlogPostCategoryId = model.BlogPostCategoryId,
                    Body = model.Body,
                    BodyOverview = model.BodyOverview,
                    Tags = model.Tags,
                    AllowComments = model.AllowComments,
                    IncludeInSitemap = model.IncludeInSitemap,
                    EndDateUtc = (StartDateUtc == EndDateUtc ? null : StartDateUtc),
                    StartDateUtc = (StartDateUtc == EndDateUtc ? null : EndDateUtc),
                    CreatedOnUtc = DateTime.Now,
                    IPAddress = _ipAddress,
                    CreatedByUserID = _user_id,
                    LanguageId = 0,
                    MetaDescription = model.MetaDescription,
                    MetaKeywords = model.MetaKeywords,
                    MetaTitle = model.MetaTitle,
                });
            }
            else
            {
                var data = await _db.BlogPosts.FindAsync(model.Id);
                if (data == null)
                {
                    return Json("NotFound");
                }
                data.Title = model.Title;
                data.BlogPostCategoryId = model.BlogPostCategoryId;
                data.Body = model.Body;
                data.BodyOverview = model.BodyOverview;
                data.Tags = model.Tags;
                data.AllowComments = model.AllowComments;
                data.IncludeInSitemap = model.IncludeInSitemap;
                data.EndDateUtc = (StartDateUtc == EndDateUtc ? null : StartDateUtc);
                data.StartDateUtc = (StartDateUtc == EndDateUtc ? null : EndDateUtc);
                data.CreatedOnUtc = DateTime.Now;
                data.IPAddress = _ipAddress;
                data.CreatedByUserID = _user_id;
                data.LanguageId = 0;
                data.MetaDescription = model.MetaDescription;
                data.MetaKeywords = model.MetaKeywords;
                data.MetaTitle = model.MetaTitle;
            }
            try
            {
                await _db.SaveChangesAsync();
                return Json("Success");
                
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
            return Json("Error");
        }
        #endregion
    }
}

