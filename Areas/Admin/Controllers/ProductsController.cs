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
    public class ProductsController : Controller
    {
        private readonly DbMyShopContext _context;

        public ProductsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int? categoryId, string? name, int page = 1, int pageSize = 30)
        {
            LoadCategories(); // 🔥 LUÔN LOAD CATEGORY

            var query = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Faculty)
                .AsNoTracking()
                .AsQueryable();

            // 🔹 LỌC THEO CATEGORY
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            // 🔹 LỌC THEO TÊN
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(name.ToLower().Trim()));
            }

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SelectedCategoryId = categoryId; // để giữ dropdown
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(data);
        }


        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            LoadCategories();
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            // --- Xử lý upload ảnh chính ---
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var fileName = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    model.Image = fileName;
                }
            }
            var exists = await _context.Products.AnyAsync(p => p.Tag == model.Tag);
            if (exists)
            {
                LoadCategories();
                ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name");
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name");
            LoadCategories();
            return View(model);
        }


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();
            LoadCategories();
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name" , product.FacultyID);
            return View(product);
        }


        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var exists = await _context.Products.AnyAsync(p => p.Tag == product.Tag && p.Id != product.Id);

            if (exists)
            {
                LoadCategories();
                ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name", product.FacultyID);
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng nhập tên khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // --- XỬ LÝ ẢNH CHÍNH ---
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            product.Image = "/" + fileName; // Lưu đường dẫn ảnh
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyID", "Name", product.FacultyID);
            LoadCategories();
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .Include(p => p.CommentPros)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // ✅ CHỈ kiểm tra OrderDetails
            bool hasOrder = _context.Enrollments.Any(e => e.ProductId == id);
            if (hasOrder)
            {
                TempData["Error"] = "Khóa học đang có học viên, không thể xóa!";
                return RedirectToAction("Index");
            }

            // ✅ Xóa con trước (tránh FK)
            if (product.CommentPros != null && product.CommentPros.Any())
            {
                _context.CommentPros.RemoveRange(product.CommentPros);
            }

            // ✅ Xóa sản phẩm
            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["Success"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private void LoadCategories()
        {
            var categories = _context.Categories
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.Level))
                .OrderBy(x => x.Level)
                .ToList();

            var result = new List<SelectListItem>();

            // Lấy danh mục gốc (cấp 1)
            var roots = categories.Where(x => x.Level.Length == 5);

            foreach (var root in roots)
            {
                BuildCategoryTree(categories, result, root, "");
            }

            ViewBag.Categories = result;
        }


        private void BuildCategoryTree(
    List<Category> source,
    List<SelectListItem> result,
    Category current,
    string parentPath)
        {
            // Build URL
            string currentPath = string.IsNullOrEmpty(parentPath)
                ? "/san-pham/" + current.Tag
                : parentPath + "/" + current.Tag;

            // Tính cấp
            int depth = current.Level.Length / 5 - 1;
            string prefix = depth > 0 ? new string('—', depth) + " " : "";

            // Add current item
            result.Add(new SelectListItem
            {
                Text = prefix + current.Name,
                Value = current.Id.ToString()
            });

            // Lấy con trực tiếp
            var children = source.Where(x =>
                x.Level.StartsWith(current.Level) &&
                x.Level.Length == current.Level.Length + 5
            );

            foreach (var child in children)
            {
                BuildCategoryTree(source, result, child, currentPath);
            }
        }
    }
}
