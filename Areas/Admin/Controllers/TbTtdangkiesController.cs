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
    public class TbTtdangkiesController : Controller
    {
        private readonly DbMyShopContext _context;

        public TbTtdangkiesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/TbTtdangkies
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.TbTtdangkies.Include(t => t.MaSkNavigation);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/TbTtdangkies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTtdangky = await _context.TbTtdangkies
                .Include(t => t.MaSkNavigation)
                .FirstOrDefaultAsync(m => m.Idtt == id);
            if (tbTtdangky == null)
            {
                return NotFound();
            }

            return View(tbTtdangky);
        }

        // GET: Admin/TbTtdangkies/Create
        public IActionResult Create()
        {
            ViewData["MaSk"] = new SelectList(_context.TbSukiens, "MaSk", "MaSk");
            return View();
        }

        // POST: Admin/TbTtdangkies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idtt,Nhan,Kieudieukhien,Rong,Cao,Trong,Thutu,MaSk")] TbTtdangky tbTtdangky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTtdangky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSk"] = new SelectList(_context.TbSukiens, "MaSk", "MaSk", tbTtdangky.MaSk);
            return View(tbTtdangky);
        }

        // GET: Admin/TbTtdangkies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTtdangky = await _context.TbTtdangkies.FindAsync(id);
            if (tbTtdangky == null)
            {
                return NotFound();
            }
            ViewData["MaSk"] = new SelectList(_context.TbSukiens, "MaSk", "MaSk", tbTtdangky.MaSk);
            return View(tbTtdangky);
        }

        // POST: Admin/TbTtdangkies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idtt,Nhan,Kieudieukhien,Rong,Cao,Trong,Thutu,MaSk")] TbTtdangky tbTtdangky)
        {
            if (id != tbTtdangky.Idtt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTtdangky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTtdangkyExists(tbTtdangky.Idtt))
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
            ViewData["MaSk"] = new SelectList(_context.TbSukiens, "MaSk", "MaSk", tbTtdangky.MaSk);
            return View(tbTtdangky);
        }

        // GET: Admin/TbTtdangkies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTtdangky = await _context.TbTtdangkies
                .Include(t => t.MaSkNavigation)
                .FirstOrDefaultAsync(m => m.Idtt == id);
            if (tbTtdangky == null)
            {
                return NotFound();
            }

            return View(tbTtdangky);
        }

        // POST: Admin/TbTtdangkies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbTtdangky = await _context.TbTtdangkies.FindAsync(id);
            if (tbTtdangky != null)
            {
                _context.TbTtdangkies.Remove(tbTtdangky);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTtdangkyExists(int id)
        {
            return _context.TbTtdangkies.Any(e => e.Idtt == id);
        }
    }
}
