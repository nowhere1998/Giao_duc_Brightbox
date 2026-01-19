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
    public class DocumentTypesController : Controller
    {
        private readonly DbMyShopContext _context;

        public DocumentTypesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/DocumentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentTypes.ToListAsync());
        }

        // GET: Admin/DocumentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            return View(documentType);
        }

        // GET: Admin/DocumentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DocumentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ord,Active,Lang")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: Admin/DocumentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }
            return View(documentType);
        }

        // POST: Admin/DocumentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ord,Active,Lang")] DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentTypeExists(documentType.Id))
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
            return View(documentType);
        }

        // GET: Admin/DocumentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            return View(documentType);
        }

        // POST: Admin/DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType != null)
            {
                _context.DocumentTypes.Remove(documentType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentTypeExists(int id)
        {
            return _context.DocumentTypes.Any(e => e.Id == id);
        }
    }
}
