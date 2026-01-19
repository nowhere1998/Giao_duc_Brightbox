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
    public class TbCountersController : Controller
    {
        private readonly DbMyShopContext _context;

        public TbCountersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/TbCounters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCounters.ToListAsync());
        }

        // GET: Admin/TbCounters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCounter = await _context.TbCounters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCounter == null)
            {
                return NotFound();
            }

            return View(tbCounter);
        }

        // GET: Admin/TbCounters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TbCounters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tenweb,Lanxem,Ngaytao")] TbCounter tbCounter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCounter);
        }

        // GET: Admin/TbCounters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCounter = await _context.TbCounters.FindAsync(id);
            if (tbCounter == null)
            {
                return NotFound();
            }
            return View(tbCounter);
        }

        // POST: Admin/TbCounters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tenweb,Lanxem,Ngaytao")] TbCounter tbCounter)
        {
            if (id != tbCounter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCounter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCounterExists(tbCounter.Id))
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
            return View(tbCounter);
        }

        // GET: Admin/TbCounters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCounter = await _context.TbCounters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCounter == null)
            {
                return NotFound();
            }

            return View(tbCounter);
        }

        // POST: Admin/TbCounters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCounter = await _context.TbCounters.FindAsync(id);
            if (tbCounter != null)
            {
                _context.TbCounters.Remove(tbCounter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCounterExists(int id)
        {
            return _context.TbCounters.Any(e => e.Id == id);
        }
    }
}
