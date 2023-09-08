using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using WPVE.Core.Domain.Catalog;
using WPVE.Data;

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ManufacturerController : Controller
    {


        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _user_id;
        private string _ipAddress;

        #endregion

        #region Ctor
        public ManufacturerController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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
              return _context.Manufacturers != null ? 
                          View(await _context.Manufacturers.Where(x=>x.Deleted ==false).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Manufacturers'  is null.");
        }


        // GET: Admin/Manufacturer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manufacturer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {

            manufacturer.Id = Guid.NewGuid().ToString();
            manufacturer.CreatedOnUtc = DateTime.Now;
            manufacturer.IPAddress = _ipAddress;
            manufacturer.Deleted = false;
           
                _context.Add(manufacturer);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(manufacturer);
        }

        // GET: Admin/Manufacturer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Manufacturers == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        // POST: Admin/Manufacturer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }
                try
                {
                    manufacturer.UpdatedOnUtc = DateTime.Now;
                    manufacturer.IPAddress = _ipAddress;
                    _context.Update(manufacturer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.Id))
                    {
                        return NotFound();
                    }
                   
                }


            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Manufacturer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Manufacturers == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Admin/Manufacturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Manufacturers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Manufacturers'  is null.");
            }
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                manufacturer.Deleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(string id)
        {
          return (_context.Manufacturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
