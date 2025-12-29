using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PagesController : Controller
    {
        private readonly DbMyShopContext _context;
        static string Level = "";
        public PagesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Pages
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Pages.OrderBy(x => x.Level).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim())).OrderBy(x => x.Level);
            }
            // Tổng số bản ghi sau khi lọc
            var totalCount = await query.CountAsync();

            // Lấy dữ liệu từng trang
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Gửi biến qua View
            ViewData["SearchName"] = name;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return View(data);
        }

        // GET: Admin/Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/Pages/Create
        public IActionResult Create(string? strLevel)
        {
            LoadCategories();
            if (strLevel != null)
                Level = strLevel;
            return View();
        }


        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page model)
        {
            var exists = await _context.Pages.AnyAsync(p => p.Tag == model.Tag);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (!ModelState.IsValid)
            {
                LoadCategories(); // ← BẮT BUỘC
                return View(model);
            }
            // ✅ Nếu Link null hoặc rỗng → gán mặc định "/"
            if (string.IsNullOrWhiteSpace(model.Link))
            {
                model.Link = "/";
            }
            model.Level = Level + model.Level;
            model.Level = Level + "00000";
            Level = "";
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Pages/Edit/5
        public async Task<IActionResult> Edit(int? id , string? strLevel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                LoadCategories();
                return NotFound();
            }

            Level = page.Level.Substring(0, page.Level.Length - 5);
            LoadCategories();
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Page model)
        {
            if (id != model.Id)
            {
                LoadCategories();
                return NotFound();
            }

            var exists = await _context.Pages.AnyAsync(p => p.Tag == model.Tag && p.Id != model.Id);
            if (exists)
            {
                LoadCategories();
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.Level = Level + model.Level;
                    model.Level = Level + "00000";
                    Level = "";
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(model.Id))
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
            return View(model);
        }

        // GET: Admin/Pages/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _context.Pages.FirstOrDefault(a => a.Id == id);
            if (model == null) return NotFound();

            string levelPrefix = model.Level; // ví dụ "00001"

            // Xóa cha + toàn bộ con cháu
            var toDelete = _context.Pages
                .Where(a => a.Level.StartsWith(levelPrefix))
                .ToList();

            _context.Pages.RemoveRange(toDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
        private void LoadCategories()
        {
            var categories = _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Id)
                .ToList();

            var result = new List<SelectListItem>();

            var parents = categories.Where(x => x.ParentId == null);

            foreach (var parent in parents)
            {
                result.Add(new SelectListItem
                {
                    Text = parent.Name,
                    Value = "/san-pham/" + parent.Slug
                });

                foreach (var child in categories.Where(x => x.ParentId == parent.Id))
                {
                    result.Add(new SelectListItem
                    {
                        Text = "— " + child.Name,
                        Value = "/san-pham/" + parent.Slug + "/" + child.Slug
                    });
                }
            }

            ViewBag.Categories = result;
        }


    }
}
