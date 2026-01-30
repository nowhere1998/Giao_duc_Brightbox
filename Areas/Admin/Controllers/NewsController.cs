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
    public class NewsController : Controller
    {
        private readonly DbMyShopContext _context;

        public NewsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index(string? name, int? groupId, int page = 1, int pageSize = 30)
        {
            // ======================
            // 1. Load GroupNews
            // ======================
            var groupNews = await _context.GroupNews
                .AsNoTracking()
                .OrderBy(x => x.Level)
                .ThenBy(x => x.Ord)
                .ToListAsync();

            // Build SelectListItem
            List<SelectListItem> groupNewsItems = new();

            foreach (var item in groupNews)
            {
                groupNewsItems.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = StringHelper.ShowNameLevel(item.Name, item.Level),
                    Selected = (groupId == item.Id)
                });
            }

            ViewBag.GroupNews = groupNewsItems;

            // ======================
            // 2. Query News
            // ======================
            var query = _context.News
                .Include(x => x.GroupNews)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            // 🔍 Lọc theo tiêu đề
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Title.Contains(name));
            }

            // 🔍 Lọc theo GroupNews (cha → lấy cả con)
            if (groupId.HasValue)
            {
                var group = groupNews.FirstOrDefault(x => x.Id == groupId);

                if (group != null && !string.IsNullOrEmpty(group.Level))
                {
                    query = query.Where(x =>
                        x.GroupNews != null &&
                        x.GroupNews.Level.StartsWith(group.Level));
                }
            }

            // ======================
            // 3. Phân trang
            // ======================
            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // ======================
            // 4. Gửi dữ liệu qua View
            // ======================
            ViewData["SearchName"] = name;
            ViewBag.GroupId = groupId;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(data);
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.GroupNews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create

        public IActionResult Create()
        {
            ViewBag.GroupId = GetGroupNewsDropdown();

            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, IFormFile? photo)
        {


            var exists = await _context.News.AnyAsync(p => p.Tag == news.Tag);
            if (exists)
            {
                ViewBag.GroupId = GetGroupNewsDropdown();
                ModelState.AddModelError("Title", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {

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
                    news.Image = "/images/" + photo.FileName;
                }

                //// Người đăng bài = user đang login
                //var userIdClaim = User.FindFirst("UserId");
                //if (userIdClaim != null)
                //{
                //    news.PostedById = long.Parse(userIdClaim.Value);
                //}


                _context.News.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GroupId = GetGroupNewsDropdown();
            return View(news);
        }


        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewBag.GroupId = GetGroupNewsDropdown(news.GroupNewsId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, News news, IFormFile? photo, string? pictureOld)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            if (photo != null && photo.Length > 0)
            {
                // Đường dẫn lưu ảnh mới
                var filePath = Path.Combine("wwwroot/images", photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                news.Image = "/images/" + photo.FileName;
            }
            else
            {
                news.Image = pictureOld;
            }
            var exists = await _context.News.AnyAsync(p => p.Tag == news.Tag && p.Id != news.Id);
            if (exists)
            {
                ViewBag.GroupId = GetGroupNewsDropdown(news.GroupNewsId);
                ModelState.AddModelError("Title", "Tên đã tồn tại, vui lòng đổi tên khác.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            ViewBag.GroupId = GetGroupNewsDropdown(news.GroupNewsId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _context.News.FirstOrDefault(a => a.Id == id);
            if (model == null)
                return NotFound();

            _context.News.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool NewsExists(long id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        private List<SelectListItem> GetGroupNewsDropdown(int? selectedId = null)
        {
            var groupNews = _context.GroupNews
                .AsNoTracking()
                .OrderBy(x => x.Level)
                .ThenBy(x => x.Ord)
                .ToList();

            List<SelectListItem> items = new();

            foreach (var item in groupNews)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = StringHelper.ShowNameLevel(item.Name, item.Level),
                    Selected = selectedId == item.Id
                });
            }

            return items;
        }

    }
}
