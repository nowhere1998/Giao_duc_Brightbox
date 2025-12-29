using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DbMyShopContext _context;

        public UsersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Users.OrderByDescending(x => x.Id).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Username.ToLower().Contains(name.ToLower().Trim())).OrderByDescending(x => x.Id);
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

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = Cipher.GenerateMD5(user.PasswordHash);
                user.Status = 1;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // lấy mật khẩu cũ từ DB
                    var oldPassword = await _context.Users
                        .Where(x => x.Id == user.Id)
                        .Select(x => x.PasswordHash)
                        .FirstOrDefaultAsync();

                    // 👉 xử lý mật khẩu
                    if (string.IsNullOrWhiteSpace(user.PasswordHash))
                    {
                        // không nhập → giữ mật khẩu cũ
                        user.PasswordHash = oldPassword;
                    }
                    else
                    {
                        // có nhập → hash mật khẩu mới
                        user.PasswordHash = Cipher.GenerateMD5(user.PasswordHash);
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public IActionResult Delete(int id)
        {
            if (!int.TryParse(User.FindFirstValue("UserId"), out int currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            // ❌ Không cho xoá chính mình
            if (id == currentUserId)
            {
                TempData["Error"] = "Không thể xoá tài khoản đang đăng nhập.";
                return RedirectToAction("Index");
            }

            var model = _context.Users.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            _context.Users.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
