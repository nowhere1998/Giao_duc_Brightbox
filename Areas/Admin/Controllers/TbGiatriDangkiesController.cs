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
    public class TbGiatriDangkiesController : Controller
    {
        private readonly DbMyShopContext _context;

        public TbGiatriDangkiesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/TbGiatriDangkies
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.TbGiatriDangkies.Include(t => t.IdttNavigation);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/TbGiatriDangkies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbGiatriDangky = await _context.TbGiatriDangkies
                .Include(t => t.IdttNavigation)
                .FirstOrDefaultAsync(m => m.Idgt == id);
            if (tbGiatriDangky == null)
            {
                return NotFound();
            }

            return View(tbGiatriDangky);
        }

        // GET: Admin/TbGiatriDangkies/Create
        public IActionResult Create()
        {
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt");
            return View();
        }

        // POST: Admin/TbGiatriDangkies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idgt,Idtt,Giatri,NgayDk")] TbGiatriDangky tbGiatriDangky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbGiatriDangky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbGiatriDangky.Idtt);
            return View(tbGiatriDangky);
        }

        // GET: Admin/TbGiatriDangkies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbGiatriDangky = await _context.TbGiatriDangkies.FindAsync(id);
            if (tbGiatriDangky == null)
            {
                return NotFound();
            }
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbGiatriDangky.Idtt);
            return View(tbGiatriDangky);
        }

        // POST: Admin/TbGiatriDangkies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idgt,Idtt,Giatri,NgayDk")] TbGiatriDangky tbGiatriDangky)
        {
            if (id != tbGiatriDangky.Idgt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbGiatriDangky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbGiatriDangkyExists(tbGiatriDangky.Idgt))
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
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbGiatriDangky.Idtt);
            return View(tbGiatriDangky);
        }

        // GET: Admin/TbGiatriDangkies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbGiatriDangky = await _context.TbGiatriDangkies
                .Include(t => t.IdttNavigation)
                .FirstOrDefaultAsync(m => m.Idgt == id);
            if (tbGiatriDangky == null)
            {
                return NotFound();
            }

            return View(tbGiatriDangky);
        }

        // POST: Admin/TbGiatriDangkies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbGiatriDangky = await _context.TbGiatriDangkies.FindAsync(id);
            if (tbGiatriDangky != null)
            {
                _context.TbGiatriDangkies.Remove(tbGiatriDangky);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbGiatriDangkyExists(long id)
        {
            return _context.TbGiatriDangkies.Any(e => e.Idgt == id);
        }
    }
}
