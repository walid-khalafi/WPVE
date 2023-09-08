using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WPVE.Core.Domain.Catalog;
using WPVE.Data;

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductCategoryController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _user_id;
        private string _ipAddress;

        #endregion

        #region Ctor
        public ProductCategoryController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _ipAddress = _httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _user_id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        #endregion



        // GET: Admin/Manufacturer
        public async Task<IActionResult> Index()
        {
            return _context.ProductCategories != null ?
                        View(await _context.ProductCategories.Where(x => x.Deleted == false).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Manufacturers'  is null.");
        }



        // GET: Admin/ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {

            productCategory.Id = Guid.NewGuid().ToString();
            productCategory.CreatedOnUtc = DateTime.Now;
            productCategory.IPAddress = _ipAddress;
            productCategory.Deleted = false;

            _context.Add(productCategory);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return NotFound();
            }
            try
            {
                productCategory.UpdatedOnUtc = DateTime.Now;
                productCategory.IPAddress = _ipAddress;
                _context.Update(productCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(productCategory.Id))
                {
                    return NotFound();
                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductCategory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: Admin/ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ProductCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
            }
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory != null)
            {
                productCategory.Deleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(string id)
        {
          return (_context.ProductCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
