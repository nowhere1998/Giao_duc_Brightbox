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
    public class GroupSupportsController : Controller
    {
        private readonly DbMyShopContext _context;

        public GroupSupportsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupSupports
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupSupports.ToListAsync());
        }

        // GET: Admin/GroupSupports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSupport = await _context.GroupSupports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupSupport == null)
            {
                return NotFound();
            }

            return View(groupSupport);
        }

        // GET: Admin/GroupSupports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GroupSupports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ord,Active,Lang")] GroupSupport groupSupport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupSupport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupSupport);
        }

        // GET: Admin/GroupSupports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSupport = await _context.GroupSupports.FindAsync(id);
            if (groupSupport == null)
            {
                return NotFound();
            }
            return View(groupSupport);
        }

        // POST: Admin/GroupSupports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ord,Active,Lang")] GroupSupport groupSupport)
        {
            if (id != groupSupport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupSupport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupSupportExists(groupSupport.Id))
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
            return View(groupSupport);
        }

        // GET: Admin/GroupSupports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSupport = await _context.GroupSupports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupSupport == null)
            {
                return NotFound();
            }

            return View(groupSupport);
        }

        // POST: Admin/GroupSupports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupSupport = await _context.GroupSupports.FindAsync(id);
            if (groupSupport != null)
            {
                _context.GroupSupports.Remove(groupSupport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupSupportExists(int id)
        {
            return _context.GroupSupports.Any(e => e.Id == id);
        }
    }
}
