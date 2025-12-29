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
    public class ProductSpecsController : Controller
    {
        private readonly DbMyShopContext _context;

        public ProductSpecsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductSpecs
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 30)
        {
            var query = _context.ProductSpecs.Include(x=>x.Product).OrderByDescending(x => x.Id).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.SpecName.ToLower().Contains(name.ToLower().Trim())).OrderByDescending(x => x.Id);
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

        // GET: Admin/ProductSpecs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpec = await _context.ProductSpecs
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSpec == null)
            {
                return NotFound();
            }

            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductSpecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,SpecName,SpecValue")] ProductSpec productSpec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSpec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpec = await _context.ProductSpecs.FindAsync(id);
            if (productSpec == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // POST: Admin/ProductSpecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProductId,SpecName,SpecValue")] ProductSpec productSpec)
        {
            if (id != productSpec.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSpec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecExists(productSpec.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Delete/5
        public IActionResult Delete(int id)
        {
            ProductSpec model = _context.ProductSpecs.FirstOrDefault(a => a.Id == id);
            // Xoá bản ghi
            _context.ProductSpecs.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ProductSpecExists(long id)
        {
            return _context.ProductSpecs.Any(e => e.Id == id);
        }
    }
}
