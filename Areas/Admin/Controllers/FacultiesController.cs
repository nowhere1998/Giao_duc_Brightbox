using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultiesController : Controller
    {
        private readonly DbMyShopContext _context;

        public FacultiesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Faculties
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Faculties.OrderByDescending(x => x.FacultyID).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim())).OrderByDescending(x => x.FacultyID);
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

        // GET: Admin/Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyID == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Admin/Faculties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty model, IFormFile? photo)
        {
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
                    model.Image = "/images/" + photo.FileName;
                }
                _context.Faculties.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        // POST: Admin/Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Faculty model, IFormFile? photo, string? pictureOld)
        {
            if (id != model.FacultyID)
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

                model.Image = "/images/" + photo.FileName;
            }
            else
            {
                model.Image = pictureOld;
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(model.FacultyID))
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
        // GET: Admin/Faculties/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _context.Faculties.FirstOrDefault(a => a.FacultyID == id);
            if (model == null) return NotFound();

            bool hasPro = _context.Products.Any(c => c.FacultyID == id);
            if (hasPro)
            {
                TempData["Error"] = "Giảng viên đang có khóa học, không thể xóa!";
                return RedirectToAction("Index");
            }
            _context.Faculties.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.FacultyID == id);
        }
    }
}
