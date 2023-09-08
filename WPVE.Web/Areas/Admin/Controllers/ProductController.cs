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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productCategories = await Task.FromResult(_db.ProductCategories.ToList());
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
            ViewData["ProductCategories"] = productCategories;
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

