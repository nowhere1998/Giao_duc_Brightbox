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
    public class CustomersController : Controller
    {
        private readonly DbMyShopContext _context;

        public CustomersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Customers
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Customers.OrderByDescending(x => x.CustomerId).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.FullName.ToLower().Contains(name.ToLower().Trim())).OrderByDescending(x => x.CustomerId);
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

        // GET: Admin/Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.PasswordHash = Cipher.GenerateMD5(customer.PasswordHash);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // lấy mật khẩu cũ từ DB
                    var oldPassword = await _context.Customers
                        .Where(x => x.CustomerId == customer.CustomerId)
                        .Select(x => x.PasswordHash)
                        .FirstOrDefaultAsync();

                    // 👉 xử lý mật khẩu
                    if (string.IsNullOrWhiteSpace(customer.PasswordHash))
                    {
                        // không nhập → giữ mật khẩu cũ
                        customer.PasswordHash = oldPassword;
                    }
                    else
                    {
                        // có nhập → hash mật khẩu mới
                        customer.PasswordHash = Cipher.GenerateMD5(customer.PasswordHash);
                    }
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public IActionResult Delete(int id)
        {
            // 1️⃣ Kiểm tra khách hàng có đơn hàng không
            bool hasOrder = _context.Orders
                .Any(o => o.CustomerId == id);

            if (hasOrder)
            {
                TempData["Error"] = "Không thể xoá khách hàng đã hoặc đang đặt hàng.";
                return RedirectToAction("Index");
            }

            // 2️⃣ Lấy khách hàng
            var model = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (model == null)
                return NotFound();

            // 3️⃣ Xoá
            _context.Customers.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
