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
    public class SupportsController : Controller
    {
        private readonly DbMyShopContext _context;

        public SupportsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Supports
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.Supports.Include(s => s.GroupSupport);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/Supports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports
                .Include(s => s.GroupSupport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (support == null)
            {
                return NotFound();
            }

            return View(support);
        }

        // GET: Admin/Supports/Create
        public IActionResult Create()
        {
            ViewData["GroupSupportId"] = new SelectList(_context.GroupSupports, "Id", "Id");
            return View();
        }

        // POST: Admin/Supports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tel,Type,Nick,Ord,Active,GroupSupportId,Lang,Priority")] Support support)
        {
            if (ModelState.IsValid)
            {
                _context.Add(support);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupSupportId"] = new SelectList(_context.GroupSupports, "Id", "Id", support.GroupSupportId);
            return View(support);
        }

        // GET: Admin/Supports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports.FindAsync(id);
            if (support == null)
            {
                return NotFound();
            }
            ViewData["GroupSupportId"] = new SelectList(_context.GroupSupports, "Id", "Id", support.GroupSupportId);
            return View(support);
        }

        // POST: Admin/Supports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tel,Type,Nick,Ord,Active,GroupSupportId,Lang,Priority")] Support support)
        {
            if (id != support.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(support);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportExists(support.Id))
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
            ViewData["GroupSupportId"] = new SelectList(_context.GroupSupports, "Id", "Id", support.GroupSupportId);
            return View(support);
        }

        // GET: Admin/Supports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports
                .Include(s => s.GroupSupport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (support == null)
            {
                return NotFound();
            }

            return View(support);
        }

        // POST: Admin/Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var support = await _context.Supports.FindAsync(id);
            if (support != null)
            {
                _context.Supports.Remove(support);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportExists(int id)
        {
            return _context.Supports.Any(e => e.Id == id);
        }
    }
}
