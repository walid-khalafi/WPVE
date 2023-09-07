using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ApplicationDbContext _context;

        public ProductTagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductTag
        public async Task<IActionResult> Index()
        {
              return _context.ProductTags != null ? 
                          View(await _context.ProductTags.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductTags'  is null.");
        }

        // GET: Admin/ProductTag/Details/5
        public async Task<IActionResult> Details(string id)
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
        public async Task<IActionResult> Create([Bind("Naame,Id,CreatedOnUtc,UpdatedOnUtc,IPAddress")] ProductTag productTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTag);
        }

        // GET: Admin/ProductTag/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ProductTags == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTags.FindAsync(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        // POST: Admin/ProductTag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Naame,Id,CreatedOnUtc,UpdatedOnUtc,IPAddress")] ProductTag productTag)
        {
            if (id != productTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagExists(productTag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
