using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPVE.Web.Areas.Admin.Models.BlogViewModel;
using WPVE.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _db;
        #endregion

        #region Ctor
        public BlogController(ApplicationDbContext db) {
            _db = db;
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

        public IActionResult AddPostOrUpdate() {
            return View();
        }
        #endregion
    }
}

