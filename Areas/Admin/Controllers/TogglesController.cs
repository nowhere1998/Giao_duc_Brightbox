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
    public class TogglesController : Controller
    {
        private readonly DbMyShopContext _context;

        public TogglesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Toggles
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.Toggles.Include(t => t.News);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/Toggles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toggle = await _context.Toggles
                .Include(t => t.News)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toggle == null)
            {
                return NotFound();
            }

            return View(toggle);
        }

        // GET: Admin/Toggles/Create
        public IActionResult Create()
        {
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id");
            return View();
        }

        // POST: Admin/Toggles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Detail,NewsId,Ord,Active")] Toggle toggle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toggle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", toggle.NewsId);
            return View(toggle);
        }

        // GET: Admin/Toggles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toggle = await _context.Toggles.FindAsync(id);
            if (toggle == null)
            {
                return NotFound();
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", toggle.NewsId);
            return View(toggle);
        }

        // POST: Admin/Toggles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Detail,NewsId,Ord,Active")] Toggle toggle)
        {
            if (id != toggle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toggle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToggleExists(toggle.Id))
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
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", toggle.NewsId);
            return View(toggle);
        }

        // GET: Admin/Toggles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toggle = await _context.Toggles
                .Include(t => t.News)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toggle == null)
            {
                return NotFound();
            }

            return View(toggle);
        }

        // POST: Admin/Toggles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var toggle = await _context.Toggles.FindAsync(id);
            if (toggle != null)
            {
                _context.Toggles.Remove(toggle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToggleExists(long id)
        {
            return _context.Toggles.Any(e => e.Id == id);
        }
    }
}
