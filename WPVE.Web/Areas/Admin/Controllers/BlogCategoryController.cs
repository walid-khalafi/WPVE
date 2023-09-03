using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WPVE.Core.Domain.Blogs;
using WPVE.Data;

namespace WPVE.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogCategory
        public async Task<IActionResult> Index()
        {
              return _context.blogCategories != null ? 
                          View(await _context.blogCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.blogCategories'  is null.");
        }

  
        // GET: Admin/BlogCategory/Create
        public async Task<IActionResult> Create()
        {
            var categories =await _context.blogCategories.ToListAsync();
            ViewData["ParentId"] = new SelectList(categories, "Id", "Title");

            return View();
        }

        // POST: Admin/BlogCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategory blogCategory)
        {
            try
            {
                blogCategory.Id = Guid.NewGuid().ToString();
                blogCategory.CreatedOnUtc = DateTime.Now;
                blogCategory.IPAddress = "";
                if (blogCategory.ParentId == null)
                {
                    blogCategory.ParentId = Guid.Empty.ToString();
                }
                _context.Add(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
              
            
            var categories = await _context.blogCategories.ToListAsync();
            ViewData["ParentId"] = new SelectList(categories, "Id", "Title");
            return View(blogCategory);
        }

        // GET: Admin/BlogCategory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.blogCategories == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.blogCategories.FindAsync(id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            var categories = await _context.blogCategories.ToListAsync();
            ViewData["ParentId"] = new SelectList(categories, "Id", "Title",blogCategory.ParentId);

            return View(blogCategory);
        }

        // POST: Admin/BlogCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BlogCategory blogCategory)
        {
            if (id != blogCategory.Id)
            {
                return NotFound();
            }
            try
            {
                blogCategory.CreatedOnUtc = DateTime.Now;
                blogCategory.IPAddress = "";
                if (blogCategory.ParentId == null)
                {
                    blogCategory.ParentId = Guid.Empty.ToString();
                }
                _context.Update(blogCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogCategoryExists(blogCategory.Id))
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

        // GET: Admin/BlogCategory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.blogCategories == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.blogCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: Admin/BlogCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.blogCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.blogCategories'  is null.");
            }
            var blogCategory = await _context.blogCategories.FindAsync(id);
            if (blogCategory != null)
            {
                //check if category have post
                var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.BlogPostCategoryId == blogCategory.Id);
                if (blogPost == null)
                {
                    _context.blogCategories.Remove(blogCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TempData["error_msg"] = "این گروه دارای محتوا می باشد شما قادر به حذف آن نمی باشید!";
                }
            }
            
           
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(string id)
        {
          return (_context.blogCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
