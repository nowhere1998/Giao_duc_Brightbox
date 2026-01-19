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
    public class CateRssesController : Controller
    {
        private readonly DbMyShopContext _context;

        public CateRssesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/CateRsses
        public async Task<IActionResult> Index()
        {
            return View(await _context.CateRsses.ToListAsync());
        }

        // GET: Admin/CateRsses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateRss = await _context.CateRsses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateRss == null)
            {
                return NotFound();
            }

            return View(cateRss);
        }

        // GET: Admin/CateRsses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CateRsses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cateid,Rsslink,Beginstring,Endstring,Source,Urlimg,Urlimgupdate,Urlimgold,Ulrimgnew,Imgfolderpath,Active")] CateRss cateRss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cateRss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cateRss);
        }

        // GET: Admin/CateRsses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateRss = await _context.CateRsses.FindAsync(id);
            if (cateRss == null)
            {
                return NotFound();
            }
            return View(cateRss);
        }

        // POST: Admin/CateRsses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cateid,Rsslink,Beginstring,Endstring,Source,Urlimg,Urlimgupdate,Urlimgold,Ulrimgnew,Imgfolderpath,Active")] CateRss cateRss)
        {
            if (id != cateRss.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cateRss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CateRssExists(cateRss.Id))
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
            return View(cateRss);
        }

        // GET: Admin/CateRsses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateRss = await _context.CateRsses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateRss == null)
            {
                return NotFound();
            }

            return View(cateRss);
        }

        // POST: Admin/CateRsses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cateRss = await _context.CateRsses.FindAsync(id);
            if (cateRss != null)
            {
                _context.CateRsses.Remove(cateRss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CateRssExists(int id)
        {
            return _context.CateRsses.Any(e => e.Id == id);
        }
    }
}
