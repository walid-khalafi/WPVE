using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WPVE.Core.Domain.Catalog;
using WPVE.Data;
using WPVE.Web.Areas.Admin.Models.BlogViewModel;
using WPVE.Web.Areas.Admin.Models.ProductViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private string _user_id;
        private string _ipAddress;

        #endregion
        #region Ctor
        public ProductController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = webHostEnvironment;
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

        [HttpPost("Admin/Product/GetPosts")]
        public async Task<IActionResult> GetPostsAsync() {

            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            var products = await Task.FromResult(_db.Products.Select(item => new ProductIndexViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ManufacturerId = item.ManufacturerId,
                ProductCategories = item.ProductCategories,
                StockQuantity = item.StockQuantity,
                 Published = item.Published
            }));

            var filteredProducts = FilterProducts(products, searchValue);
            var sortedProducts = SortProducts(filteredProducts, sortColumnName, sortDirection);
            var pagedPosts = sortedProducts.Skip(start).Take(length);

            int recordsTotal = products.Count();
            int recordsFilteredTotal = filteredProducts.Count();

            return Json(new
            {
                data = pagedPosts,
                draw = Request.Form["draw"],
                recordsTotal,
                recordsFiltered = recordsFilteredTotal
            });
        }

        private IEnumerable<ProductIndexViewModel> FilterProducts(IEnumerable<ProductIndexViewModel> products, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                string searchLower = searchValue.ToLower();
                products = products.Where(x => x.Name.ToLower().Contains(searchLower));
            }
            return products;
        }

        private IEnumerable<ProductIndexViewModel> SortProducts(IEnumerable<ProductIndexViewModel> posts, string sortColumnName, string sortDirection)
        {
            switch (sortColumnName)
            {
                case "Name":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.Name) : posts.OrderByDescending(x => x.Name);
                    break;
                case "Price":
                    posts = sortDirection == "asc" ? posts.OrderBy(x => x.Price) : posts.OrderByDescending(x => x.Price);
                    break;
            }
            return posts;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productCategories = await Task.FromResult(_db.ProductCategories.Where(x=>!x.Deleted).ToList());
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
            ViewData["ProductCategories"] = productCategories;

            var manufacturers = await Task.FromResult(_db.Manufacturers.Where(x=>!x.Deleted).ToList());
            if (manufacturers == null)
            {
                manufacturers = new List<Manufacturer>();
            }
            ViewData["ManufacturerId"] = new SelectList(manufacturers, "Id", "Name");
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> Create(IFormFile[] file,Product model) {

            if (model == null)
            {
                BadRequest();
            }
            model.Id = Guid.NewGuid().ToString();
            model.CreatedOnUtc = DateTime.Now;
            model.CreatedByUserID = _user_id;
            model.IPAddress = _ipAddress;
 
            //upload file

            if (file !=null)
            {
                long size = file.Sum(f => f.Length);

                // user projects folder path
                string product_attachments_folder_path = "";
                try
                {
                    product_attachments_folder_path = Path.Combine(this._hostingEnvironment.ContentRootPath, "Attachments","Products", model.Id);
                    // create user projects directory if not exist
                    if (!Directory.Exists(product_attachments_folder_path))
                    {
                        Directory.CreateDirectory(product_attachments_folder_path);
                    }

                    foreach (var formFile in file)
                    {

                        var file_path = Path.Combine(product_attachments_folder_path, formFile.FileName);
                        model.Pictures += $"{file_path},";
                        string[] allowed_types = {
                            "image/png",
                            "image/jpeg",
                            "image/gif",
                        };
                        var type_allowed = allowed_types.FirstOrDefault(x => x.Contains(formFile.ContentType));

                        if (!string.IsNullOrWhiteSpace(type_allowed)) {

                            using (var stream = new FileStream(file_path, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);

                            }
                        }

                    }

                    _db.Products.Add(model);
                    await _db.SaveChangesAsync();
                    TempData["success_msg"] = "محصول مورد نظر با موفقیت ذخیره شد";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["error_msg"] = $"{ex.Message}";
                }
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}

