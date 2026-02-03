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
    public class EnrollmentsController : Controller
    {
        private readonly DbMyShopContext _context;

        public EnrollmentsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Enrollments
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.Enrollments.Include(x => x.Customer).Include(x=> x.Product).OrderByDescending(x => x.Id).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();

                query = query
                    .Include(x => x.Customer)
                    .Include(x => x.Product)
                    .Where(x => x.Customer.Name.Contains(name))
                    .OrderByDescending(x => x.Id);
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

        // GET: Admin/Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Customer)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Admin/Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,ProductId,EnrollDate,PricePaid,Status,CreatedAt,UpdatedAt")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", enrollment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", enrollment.ProductId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", enrollment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", enrollment.ProductId);
            return View(enrollment);
        }

        // POST: Admin/Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,ProductId,EnrollDate,PricePaid,Status,CreatedAt,UpdatedAt")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", enrollment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", enrollment.ProductId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Customer)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Admin/Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
