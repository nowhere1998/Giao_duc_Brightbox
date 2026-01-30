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
                user.Password = Cipher.GenerateMD5(user.Password);
                user.Active = 1;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, User user)
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
                        .Select(x => x.Password)
                        .FirstOrDefaultAsync();

                    // 👉 xử lý mật khẩu
                    if (string.IsNullOrWhiteSpace(user.Password))
                    {
                        // không nhập → giữ mật khẩu cũ
                        user.Password = oldPassword;
                    }
                    else
                    {
                        // có nhập → hash mật khẩu mới
                        user.Password = Cipher.GenerateMD5(user.Password);
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

        public IActionResult CheckPassword(int id, string password)
        {
            var user = _context.Users.Find(id);
            if (user == null) return Json(false);

            bool isValid = Cipher.GenerateMD5(password) == user.Password;
            return Json(isValid);
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

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(
            string currentPassword,
            string newPassword,
            string confirmPassword)
        {
            // 👉 Lấy UserId từ Claims
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdClaim);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy người dùng.";
                return View();
            }

            // 👉 Kiểm tra mật khẩu hiện tại
            var currentPasswordMd5 = Cipher.GenerateMD5(currentPassword);
            if (currentPasswordMd5 != user.Password)
            {
                ViewBag.ErrorMessage = "Mật khẩu hiện tại không đúng.";
                return View();
            }

            // 👉 Validate mật khẩu mới
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới và xác nhận không khớp.";
                return View();
            }

            // ❌ KHÔNG ĐƯỢC TRÙNG MẬT KHẨU CŨ
            var newPasswordMd5 = Cipher.GenerateMD5(newPassword);
            if (newPasswordMd5 == user.Password)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới không được trùng với mật khẩu cũ.";
                return View();
            }

            // ✅ Cập nhật mật khẩu mới
            user.Password = newPasswordMd5;
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Đổi mật khẩu thành công.";
            return View();
        }


        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
