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
    public class GroupNewsUsersController : Controller
    {
        private readonly DbMyShopContext _context;

        public GroupNewsUsersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupNewsUsers
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.GroupNewsUsers.Include(g => g.GroupNews).Include(g => g.User);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/GroupNewsUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupNewsUser = await _context.GroupNewsUsers
                .Include(g => g.GroupNews)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupNewsUser == null)
            {
                return NotFound();
            }

            return View(groupNewsUser);
        }

        // GET: Admin/GroupNewsUsers/Create
        public IActionResult Create()
        {
            ViewData["GroupNewsId"] = new SelectList(_context.GroupNews, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/GroupNewsUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupNewsId,UserId,Check")] GroupNewsUser groupNewsUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupNewsUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupNewsId"] = new SelectList(_context.GroupNews, "Id", "Id", groupNewsUser.GroupNewsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupNewsUser.UserId);
            return View(groupNewsUser);
        }

        // GET: Admin/GroupNewsUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupNewsUser = await _context.GroupNewsUsers.FindAsync(id);
            if (groupNewsUser == null)
            {
                return NotFound();
            }
            ViewData["GroupNewsId"] = new SelectList(_context.GroupNews, "Id", "Id", groupNewsUser.GroupNewsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupNewsUser.UserId);
            return View(groupNewsUser);
        }

        // POST: Admin/GroupNewsUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GroupNewsId,UserId,Check")] GroupNewsUser groupNewsUser)
        {
            if (id != groupNewsUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupNewsUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupNewsUserExists(groupNewsUser.Id))
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
            ViewData["GroupNewsId"] = new SelectList(_context.GroupNews, "Id", "Id", groupNewsUser.GroupNewsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", groupNewsUser.UserId);
            return View(groupNewsUser);
        }

        // GET: Admin/GroupNewsUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupNewsUser = await _context.GroupNewsUsers
                .Include(g => g.GroupNews)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupNewsUser == null)
            {
                return NotFound();
            }

            return View(groupNewsUser);
        }

        // POST: Admin/GroupNewsUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var groupNewsUser = await _context.GroupNewsUsers.FindAsync(id);
            if (groupNewsUser != null)
            {
                _context.GroupNewsUsers.Remove(groupNewsUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupNewsUserExists(long id)
        {
            return _context.GroupNewsUsers.Any(e => e.Id == id);
        }
    }
}
