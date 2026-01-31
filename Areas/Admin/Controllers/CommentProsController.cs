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
    public class CommentProsController : Controller
    {
        private readonly DbMyShopContext _context;

        public CommentProsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/CommentPros
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.CommentPros.Include(x => x.Product).OrderBy(x => x.Id).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim())).OrderBy(x => x.Id);
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

        // GET: Admin/CommentPros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentPro = await _context.CommentPros
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentPro == null)
            {
                return NotFound();
            }

            return View(commentPro);
        }

        // GET: Admin/CommentPros/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/CommentPros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Comment1,Date,Rate,CustomerId,ProductId,ImageUrl")] CommentPro commentPro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentPro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", commentPro.ProductId);
            return View(commentPro);
        }

        // GET: Admin/CommentPros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentPro = await _context.CommentPros.FindAsync(id);
            if (commentPro == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", commentPro.ProductId);
            return View(commentPro);
        }

        // POST: Admin/CommentPros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Comment1,Date,Rate,CustomerId,ProductId,ImageUrl")] CommentPro commentPro)
        {
            if (id != commentPro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentPro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentProExists(commentPro.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", commentPro.ProductId);
            return View(commentPro);
        }

        // GET: Admin/CommentPros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentPro = await _context.CommentPros
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentPro == null)
            {
                return NotFound();
            }

            return View(commentPro);
        }

        // POST: Admin/CommentPros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentPro = await _context.CommentPros.FindAsync(id);
            if (commentPro != null)
            {
                _context.CommentPros.Remove(commentPro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentProExists(int id)
        {
            return _context.CommentPros.Any(e => e.Id == id);
        }
    }
}
