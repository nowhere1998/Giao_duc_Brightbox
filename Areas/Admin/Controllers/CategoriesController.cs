using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly DbMyShopContext _context;

        public CategoriesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Categories
                .Include(x => x.Parent)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.Name!.ToLower().Contains(name.ToLower().Trim()));
            }

            var totalCount = await query.CountAsync();

            var list = await query
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 🔥 Sắp xếp CHA → CON
            var result = new List<Category>();

            var parents = list.Where(x => x.ParentId == null).ToList();

            foreach (var parent in parents)
            {
                result.Add(parent);

                var children = list
                    .Where(x => x.ParentId == parent.Id)
                    .ToList();

                result.AddRange(children);
            }

            ViewData["SearchName"] = name;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(result);
        }


        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            // Lọc các danh mục gốc (ParentId == null)
            var rootCategories = _context.Categories
                                         .Where(c => c.ParentId == null)
                                         .ToList();

            ViewData["ParentId"] = new SelectList(rootCategories, "Id", "Name");
            return View();
        }


        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var exists = await _context.Categories.AnyAsync(p => p.Slug == category.Slug);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            var exists = await _context.Categories.AnyAsync(p => p.Slug == category.Slug && p.Id != category.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Id", category.ParentId);
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public IActionResult Delete(int id)
        {
            // Lấy danh mục cha
            var parent = _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (parent == null)
                return NotFound();

            // Lấy toàn bộ danh mục con
            var children = _context.Categories
                .Include(c => c.Products)
                .Where(c => c.ParentId == id)
                .ToList();

            // Kiểm tra sản phẩm ở CHA
            if (parent.Products != null && parent.Products.Any())
            {
                TempData["Error"] = "Danh mục đang có sản phẩm, không thể xoá!";
                return RedirectToAction("Index");
            }

            // Kiểm tra sản phẩm ở CON
            if (children.Any(c => c.Products != null && c.Products.Any()))
            {
                TempData["Error"] = "Danh mục con đang có sản phẩm, không thể xoá!";
                return RedirectToAction("Index");
            }

            // Xoá con trước – cha sau
            _context.Categories.RemoveRange(children);
            _context.Categories.Remove(parent);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
