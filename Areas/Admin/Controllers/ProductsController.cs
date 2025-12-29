using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> Index(int? categoryId,string? name,int page = 1,int pageSize = 30)
        {
            // =======================
            // 1️⃣ BUILD CATEGORY CHA – CON
            // =======================
            ViewBag.CategoryId = categoryId;

            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            List<SelectListItem> categoryItems = new();

            void BuildCategory(int? parentId, string prefix)
            {
                var childs = categories
                    .Where(c => c.ParentId == parentId)
                    .OrderBy(c => c.Name)
                    .ToList();

                foreach (var c in childs)
                {
                    categoryItems.Add(new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = prefix + c.Name,
                        Selected = categoryId == c.Id
                    });

                    // Đệ quy danh mục con
                    BuildCategory(c.Id, prefix + "— ");
                }
            }

            BuildCategory(null, "");

            ViewBag.Categories = categoryItems;

            // =======================
            // 2️⃣ QUERY SẢN PHẨM
            // =======================
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Dealer)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(name.ToLower().Trim()));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x =>
                    x.CategoryId == categoryId.Value ||
                    x.Category.ParentId == categoryId.Value
                );
            }

            // =======================
            // 3️⃣ PHÂN TRANG
            // =======================
            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // =======================
            // 4️⃣ VIEW DATA
            // =======================
            ViewData["SearchName"] = name;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(data);
        }


        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Dealer)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.ParentId != null),"Id","Name");
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name");
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

            // ================================
            //      XỬ LÝ ẢNH PHỤ (Gallery)
            // ================================
            var galleryJson = HttpContext.Request.Form["GalleryImages"];

            if (!string.IsNullOrEmpty(galleryJson))
            {
                // Parse JSON thành List<string>
                var imageList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(galleryJson);

                model.ProductImages = new List<ProductImage>();

                foreach (var img in imageList)
                {
                    model.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = img  // Lưu đường dẫn ảnh phụ
                    });
                }
            }
            var exists = await _context.Products.AnyAsync(p => p.Slug == model.Slug);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", model.DealerId);
            return View(model);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                               .Include(p => p.ProductImages)
                               .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", product.DealerId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CategoryId,Name,Slug,DealerId,Price,SalePrice,Image,Description,Status,CreatedAt,UpdatedAt")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var exists = await _context.Products.AnyAsync(p => p.Slug == product.Slug && p.Id != product.Id);

            if (exists)
            {
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

                    // --- XỬ LÝ ẢNH PHỤ (Gallery) ---
                    var galleryJson = HttpContext.Request.Form["GalleryImages"];
                    if (!string.IsNullOrEmpty(galleryJson))
                    {
                        var imageList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(galleryJson);

                        // Xóa các ảnh phụ cũ
                        var oldImages = _context.ProductImages.Where(pi => pi.ProductId == product.Id);
                        _context.ProductImages.RemoveRange(oldImages);

                        // Thêm ảnh mới
                        product.ProductImages = new List<ProductImage>();
                        foreach (var img in imageList)
                        {
                            product.ProductImages.Add(new ProductImage
                            {
                                ImageUrl = img,
                                ProductId = product.Id
                            });
                        }
                    }

                    product.UpdatedAt = DateTime.Now;
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

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", product.DealerId);
            return View(product);
        }


        // GET: Admin/Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                                  .Include(p => p.ProductImages) // nếu có bảng ProductImages
                                  .FirstOrDefault(a => a.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Kiểm tra xem có ràng buộc khóa ngoại nào không
            if ((product.ProductImages != null && product.ProductImages.Any()) ||
                _context.OrderDetails.Any(od => od.ProductId == id)) // ví dụ bảng OrderDetails
            {
                TempData["Error"] = "Sản phẩm đang được sử dụng ở nơi khác, không thể xóa!";
                return RedirectToAction("Index");
            }

            // Xoá bản ghi
            _context.Products.Remove(product);

            // Nếu có trường Ord cần giảm, xử lý ở đây
            _context.SaveChanges();

            TempData["Success"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index");
        }


        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
