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
    public class GroupNewsController : Controller
    {
        private readonly DbMyShopContext _context;
        static string Level = "";
        public GroupNewsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupNews
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.GroupNews.OrderBy(x => x.Level).AsNoTracking();
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
        // GET: Admin/GroupNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupNews = await _context.GroupNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupNews == null)
            {
                return NotFound();
            }

            return View(groupNews);
        }

        // GET: Admin/GroupNews/Create
        public IActionResult Create(string? strLevel)
        {
            if (strLevel != null)
                Level = strLevel;
            return View();
        }

        // POST: Admin/GroupNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupNews model, IFormFile? photo)
        {
            // ✅ Xử lý upload ảnh
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (photo != null && photo.Length != 0)
            {
                // Lưu file và đường dẫn
                var filePath = Path.Combine("wwwroot/images", photo.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                // Gán đường dẫn cho thuộc tính Thumbnail
                model.Hinhanh = "/images/" + photo.FileName;
            }

            var exists = await _context.GroupNews.AnyAsync(p => p.Tag == model.Tag);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            model.Level = Level + model.Level;
            model.Level = Level + "00000";
            Level = "";
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/GroupNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupNews = await _context.GroupNews.FindAsync(id);
            if (groupNews == null)
            {
                return NotFound();
            }
            return View(groupNews);
        }

        // POST: Admin/GroupNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupNews model, IFormFile? photo, string? pictureOld)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var exists = await _context.GroupNews.AnyAsync(p => p.Tag == model.Tag && p.Id != model.Id);

            if (exists)
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại, vui lòng nhập tên khác.");
            }
            if (photo != null && photo.Length > 0)
            {
                // Đường dẫn lưu ảnh mới
                var filePath = Path.Combine("wwwroot/images", photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                model.Hinhanh = "/images/" + photo.FileName;
            }
            else
            {
                model.Hinhanh = pictureOld;
            }
            model.Level = Level + model.Level;
            model.Level = Level + "00000";
            Level = "";
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupNewsExists(model.Id))
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

        public IActionResult Delete(int id)
        {
            // 1️⃣ Lấy group cha
            var group = _context.GroupNews.FirstOrDefault(x => x.Id == id);
            if (group == null)
                return NotFound();

            var levelPrefix = group.Level;

            // 2️⃣ Lấy toàn bộ group (cha + con cháu)
            var groups = _context.GroupNews
                .Where(x => x.Level.StartsWith(levelPrefix))
                .ToList();

            var groupIds = groups.Select(x => x.Id).ToList();

            // 3️⃣ CHỈ kiểm tra News (giống Product kiểm OrderDetails)
            bool hasNews = _context.News
                .AsEnumerable() // ⭐ CHỐT – tránh lỗi WITH
                .Any(n => n.GroupNewsId.HasValue && groupIds.Contains(n.GroupNewsId.Value));

            if (hasNews)
            {
                TempData["Error"] = "Nhóm đang có bài viết, không thể xóa!";
                return RedirectToAction("Index");
            }

            // 4️⃣ Xóa con trước – cha sau
            var toDelete = groups
                .OrderByDescending(x => x.Level.Length)
                .ToList();

            _context.GroupNews.RemoveRange(toDelete);
            _context.SaveChanges();

            TempData["Success"] = "Xóa nhóm tin thành công!";
            return RedirectToAction("Index");
        }

        private bool GroupNewsExists(int id)
        {
            return _context.GroupNews.Any(e => e.Id == id);
        }
    }
}
