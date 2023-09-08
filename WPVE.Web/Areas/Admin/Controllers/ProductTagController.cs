using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WPVE.Core.Domain.Catalog;
using WPVE.Data;

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagController : Controller
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _user_id;
        private string _ipAddress;

        #endregion

        #region Ctor
        public ProductTagController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _ipAddress = _httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _user_id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        #endregion


        // GET: Admin/ProductTag
        public async Task<IActionResult> Index()
        {
              return _context.ProductTags != null ? 
                          View(await _context.ProductTags.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductTags'  is null.");
        }


        // GET: Admin/ProductTag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductTag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTag productTag)
        {
            productTag.Id = Guid.NewGuid().ToString();
            productTag.CreatedOnUtc = DateTime.Now;
            productTag.IPAddress = _ipAddress;
           
            try
            {
                _context.Add(productTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(productTag);
        }

        // GET: Admin/ProductTag/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ProductTags == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // POST: Admin/ProductTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ProductTags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductTags'  is null.");
            }
            var productTag = await _context.ProductTags.FindAsync(id);
            if (productTag != null)
            {
                _context.ProductTags.Remove(productTag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTagExists(string id)
        {
          return (_context.ProductTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
