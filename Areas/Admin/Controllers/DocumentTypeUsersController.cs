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
    public class DocumentTypeUsersController : Controller
    {
        private readonly DbMyShopContext _context;

        public DocumentTypeUsersController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/DocumentTypeUsers
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.DocumentTypeUsers.Include(d => d.DocumentType).Include(d => d.User);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/DocumentTypeUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentTypeUser = await _context.DocumentTypeUsers
                .Include(d => d.DocumentType)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentTypeUser == null)
            {
                return NotFound();
            }

            return View(documentTypeUser);
        }

        // GET: Admin/DocumentTypeUsers/Create
        public IActionResult Create()
        {
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/DocumentTypeUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentTypeId,UserId,Check")] DocumentTypeUser documentTypeUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentTypeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", documentTypeUser.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", documentTypeUser.UserId);
            return View(documentTypeUser);
        }

        // GET: Admin/DocumentTypeUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentTypeUser = await _context.DocumentTypeUsers.FindAsync(id);
            if (documentTypeUser == null)
            {
                return NotFound();
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", documentTypeUser.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", documentTypeUser.UserId);
            return View(documentTypeUser);
        }

        // POST: Admin/DocumentTypeUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DocumentTypeId,UserId,Check")] DocumentTypeUser documentTypeUser)
        {
            if (id != documentTypeUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentTypeUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentTypeUserExists(documentTypeUser.Id))
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
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", documentTypeUser.DocumentTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", documentTypeUser.UserId);
            return View(documentTypeUser);
        }

        // GET: Admin/DocumentTypeUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentTypeUser = await _context.DocumentTypeUsers
                .Include(d => d.DocumentType)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentTypeUser == null)
            {
                return NotFound();
            }

            return View(documentTypeUser);
        }

        // POST: Admin/DocumentTypeUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var documentTypeUser = await _context.DocumentTypeUsers.FindAsync(id);
            if (documentTypeUser != null)
            {
                _context.DocumentTypeUsers.Remove(documentTypeUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentTypeUserExists(long id)
        {
            return _context.DocumentTypeUsers.Any(e => e.Id == id);
        }
    }
}
