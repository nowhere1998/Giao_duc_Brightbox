using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProFacultiesController : Controller
    {
        private readonly DbMyShopContext _context;

        public ProFacultiesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/ProFaculties
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.ProFaculties.Include(p => p.Faculty).Include(p => p.Product);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/ProFaculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proFaculty = await _context.ProFaculties
                .Include(p => p.Faculty)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProFacultyID == id);
            if (proFaculty == null)
            {
                return NotFound();
            }

            return View(proFaculty);
        }

        // GET: Admin/ProFaculties/Create
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/ProFaculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProFacultyID,ProductId,FacultyID")] ProFaculty proFaculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proFaculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", proFaculty.FacultyID);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proFaculty.ProductId);
            return View(proFaculty);
        }

        // GET: Admin/ProFaculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proFaculty = await _context.ProFaculties.FindAsync(id);
            if (proFaculty == null)
            {
                return NotFound();
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", proFaculty.FacultyID);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proFaculty.ProductId);
            return View(proFaculty);
        }

        // POST: Admin/ProFaculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProFacultyID,ProductId,FacultyID")] ProFaculty proFaculty)
        {
            if (id != proFaculty.ProFacultyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proFaculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProFacultyExists(proFaculty.ProFacultyID))
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
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", proFaculty.FacultyID);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proFaculty.ProductId);
            return View(proFaculty);
        }

        // GET: Admin/ProFaculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proFaculty = await _context.ProFaculties
                .Include(p => p.Faculty)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProFacultyID == id);
            if (proFaculty == null)
            {
                return NotFound();
            }

            return View(proFaculty);
        }

        // POST: Admin/ProFaculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proFaculty = await _context.ProFaculties.FindAsync(id);
            if (proFaculty != null)
            {
                _context.ProFaculties.Remove(proFaculty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProFacultyExists(int id)
        {
            return _context.ProFaculties.Any(e => e.ProFacultyID == id);
        }
    }
}
