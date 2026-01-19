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
    public class DocumentsController : Controller
    {
        private readonly DbMyShopContext _context;

        public DocumentsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Documents
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.Documents.Include(d => d.Member).Include(d => d.Type);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Member)
                .Include(d => d.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Admin/Documents/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id");
            return View();
        }

        // POST: Admin/Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,CreateDate,EffectiveDate,Info,File,Priority,Active,TypeId,MemberId,Lang")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", document.MemberId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", document.TypeId);
            return View(document);
        }

        // GET: Admin/Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", document.MemberId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", document.TypeId);
            return View(document);
        }

        // POST: Admin/Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,CreateDate,EffectiveDate,Info,File,Priority,Active,TypeId,MemberId,Lang")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", document.MemberId);
            ViewData["TypeId"] = new SelectList(_context.DocumentTypes, "Id", "Id", document.TypeId);
            return View(document);
        }

        // GET: Admin/Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Member)
                .Include(d => d.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Admin/Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
