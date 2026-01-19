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
    public class AdvertisesController : Controller
    {
        private readonly DbMyShopContext _context;

        public AdvertisesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Advertises
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Advertises.OrderBy(x => x.Ord).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim())).OrderBy(x => x.Ord);
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

        // GET: Admin/Advertises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertise = await _context.Advertises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertise == null)
            {
                return NotFound();
            }

            return View(advertise);
        }

        // GET: Admin/Advertises/Create
        public IActionResult Create()
        {
            List<Page> pages = _context.Pages.ToList();
            ViewBag.Page = pages;
            return View(new Advertise
            {
                Active = true   // 👈 luôn checked
            });
        }

        // POST: Admin/Advertises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertise model)
        {
            // --- Xử lý upload ảnh ---
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

            // --- Xử lý vị trí trùng ---
            int newOrd = model.Ord ?? 1;

            // Tăng vị trí cho tất cả bản ghi có Position >= newPos
            var items = await _context.Advertises
                .Where(a => a.Ord >= newOrd)
                .ToListAsync();

            foreach (var item in items)
            {
                item.Ord += 1;
            }
            // --- Lưu bản ghi ---
            model.Ord = newOrd;
            //model.Active = true;
            _context.Advertises.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // GET: Admin/Advertises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertise = await _context.Advertises.FindAsync(id);
            if (advertise == null)
            {
                return NotFound();
            }
            List<Page> pages = _context.Pages.ToList();
            ViewBag.Page = pages;
            return View(advertise);
        }

        // POST: Admin/Advertises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Advertise model)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var FileName = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    model.Image = FileName;
                }
            }
            // XỬ LÝ ORD KHÔNG BỊ TRÙNG
            // ================================
            int newOrd = model.Ord ?? 1;

            // Lấy bản ghi cũ (trước khi sửa) để biết Ord cũ
            var old = await _context.Advertises.AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);

            // Nếu Ord thay đổi thì mới xử lý tránh đẩy lung tung
            if (old != null && old.Ord != newOrd)
            {
                // Tăng thứ tự cho tất cả bản ghi có Ord >= newOrd
                var items = await _context.Advertises
                    .Where(a => a.Id != id && a.Ord >= newOrd)
                    .ToListAsync();

                foreach (var item in items)
                {
                    item.Ord += 1;
                }

                model.Ord = newOrd;
            }
            _context.Advertises.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = _context.Advertises.FirstOrDefault(a => a.Id == id);
            if (model == null)
                return NotFound();

            _context.Advertises.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool AdvertiseExists(int id)
        {
            return _context.Advertises.Any(e => e.Id == id);
        }
    }
}
