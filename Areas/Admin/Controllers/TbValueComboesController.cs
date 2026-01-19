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
    public class TbValueComboesController : Controller
    {
        private readonly DbMyShopContext _context;

        public TbValueComboesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/TbValueComboes
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.TbValueCombos.Include(t => t.IdttNavigation);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/TbValueComboes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbValueCombo = await _context.TbValueCombos
                .Include(t => t.IdttNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbValueCombo == null)
            {
                return NotFound();
            }

            return View(tbValueCombo);
        }

        // GET: Admin/TbValueComboes/Create
        public IActionResult Create()
        {
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt");
            return View();
        }

        // POST: Admin/TbValueComboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idtt,Giatri")] TbValueCombo tbValueCombo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbValueCombo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbValueCombo.Idtt);
            return View(tbValueCombo);
        }

        // GET: Admin/TbValueComboes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbValueCombo = await _context.TbValueCombos.FindAsync(id);
            if (tbValueCombo == null)
            {
                return NotFound();
            }
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbValueCombo.Idtt);
            return View(tbValueCombo);
        }

        // POST: Admin/TbValueComboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idtt,Giatri")] TbValueCombo tbValueCombo)
        {
            if (id != tbValueCombo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbValueCombo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbValueComboExists(tbValueCombo.Id))
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
            ViewData["Idtt"] = new SelectList(_context.TbTtdangkies, "Idtt", "Idtt", tbValueCombo.Idtt);
            return View(tbValueCombo);
        }

        // GET: Admin/TbValueComboes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbValueCombo = await _context.TbValueCombos
                .Include(t => t.IdttNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbValueCombo == null)
            {
                return NotFound();
            }

            return View(tbValueCombo);
        }

        // POST: Admin/TbValueComboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbValueCombo = await _context.TbValueCombos.FindAsync(id);
            if (tbValueCombo != null)
            {
                _context.TbValueCombos.Remove(tbValueCombo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbValueComboExists(int id)
        {
            return _context.TbValueCombos.Any(e => e.Id == id);
        }
    }
}
