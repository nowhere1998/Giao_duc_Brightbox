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
    public class GroupLibraryUsersController : Controller
    {
        private readonly DbMyShopContext _context;

        public GroupLibraryUsersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupLibraryUsers
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.GroupLibraryUsers.Include(g => g.GroupLibrary).Include(g => g.User);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/GroupLibraryUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibraryUser = await _context.GroupLibraryUsers
                .Include(g => g.GroupLibrary)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupLibraryUser == null)
            {
                return NotFound();
            }

            return View(groupLibraryUser);
        }

        // GET: Admin/GroupLibraryUsers/Create
        public IActionResult Create()
        {
            ViewData["GroupLibraryId"] = new SelectList(_context.GroupLibraries, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/GroupLibraryUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupLibraryId,UserId,Check")] GroupLibraryUser groupLibraryUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupLibraryUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupLibraryId"] = new SelectList(_context.GroupLibraries, "Id", "Id", groupLibraryUser.GroupLibraryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupLibraryUser.UserId);
            return View(groupLibraryUser);
        }

        // GET: Admin/GroupLibraryUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibraryUser = await _context.GroupLibraryUsers.FindAsync(id);
            if (groupLibraryUser == null)
            {
                return NotFound();
            }
            ViewData["GroupLibraryId"] = new SelectList(_context.GroupLibraries, "Id", "Id", groupLibraryUser.GroupLibraryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupLibraryUser.UserId);
            return View(groupLibraryUser);
        }

        // POST: Admin/GroupLibraryUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GroupLibraryId,UserId,Check")] GroupLibraryUser groupLibraryUser)
        {
            if (id != groupLibraryUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupLibraryUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupLibraryUserExists(groupLibraryUser.Id))
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
            ViewData["GroupLibraryId"] = new SelectList(_context.GroupLibraries, "Id", "Id", groupLibraryUser.GroupLibraryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupLibraryUser.UserId);
            return View(groupLibraryUser);
        }

        // GET: Admin/GroupLibraryUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupLibraryUser = await _context.GroupLibraryUsers
                .Include(g => g.GroupLibrary)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupLibraryUser == null)
            {
                return NotFound();
            }

            return View(groupLibraryUser);
        }

        // POST: Admin/GroupLibraryUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var groupLibraryUser = await _context.GroupLibraryUsers.FindAsync(id);
            if (groupLibraryUser != null)
            {
                _context.GroupLibraryUsers.Remove(groupLibraryUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupLibraryUserExists(long id)
        {
            return _context.GroupLibraryUsers.Any(e => e.Id == id);
        }
    }
}
