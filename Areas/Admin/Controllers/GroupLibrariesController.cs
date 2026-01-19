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
    public class GroupLibrariesController : Controller
    {
        private readonly DbMyShopContext _context;

        public GroupLibrariesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupLibraries
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupLibraries.ToListAsync());
        }

        // GET: Admin/GroupLibraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibrary = await _context.GroupLibraries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupLibrary == null)
            {
                return NotFound();
            }

            return View(groupLibrary);
        }

        // GET: Admin/GroupLibraries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GroupLibraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tag,Level,Image,Ord,Active,Lang,Type,Date,ShowIndex,GroupId")] GroupLibrary groupLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupLibrary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupLibrary);
        }

        // GET: Admin/GroupLibraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibrary = await _context.GroupLibraries.FindAsync(id);
            if (groupLibrary == null)
            {
                return NotFound();
            }
            return View(groupLibrary);
        }

        // POST: Admin/GroupLibraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tag,Level,Image,Ord,Active,Lang,Type,Date,ShowIndex,GroupId")] GroupLibrary groupLibrary)
        {
            if (id != groupLibrary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupLibrary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupLibraryExists(groupLibrary.Id))
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
            return View(groupLibrary);
        }

        // GET: Admin/GroupLibraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibrary = await _context.GroupLibraries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupLibrary == null)
            {
                return NotFound();
            }

            return View(groupLibrary);
        }

        // POST: Admin/GroupLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupLibrary = await _context.GroupLibraries.FindAsync(id);
            if (groupLibrary != null)
            {
                _context.GroupLibraries.Remove(groupLibrary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupLibraryExists(int id)
        {
            return _context.GroupLibraries.Any(e => e.Id == id);
        }
    }
}
