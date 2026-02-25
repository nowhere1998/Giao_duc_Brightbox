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
    public class RecruitmentsController : Controller
    {
        private readonly DbMyShopContext _context;

        public RecruitmentsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Recruitments
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.Recruitments.Include(r => r.GroupRecruitment);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/Recruitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitments
                .Include(r => r.GroupRecruitment)
                .FirstOrDefaultAsync(m => m.RecruitmentId == id);
            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // GET: Admin/Recruitments/Create
        public IActionResult Create()
        {
            ViewData["GroupRecruitmentId"] = new SelectList(_context.GroupRecruitments, "Id", "Id");
            return View();
        }

        // POST: Admin/Recruitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecruitmentId,JobTitle,JobDescription,CompanyName,CompanyAddress,Phone,Email,Salary,GroupRecruitmentId,Date,Status")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recruitment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupRecruitmentId"] = new SelectList(_context.GroupRecruitments, "Id", "Id", recruitment.GroupRecruitmentId);
            return View(recruitment);
        }

        // GET: Admin/Recruitments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitments.FindAsync(id);
            if (recruitment == null)
            {
                return NotFound();
            }
            ViewData["GroupRecruitmentId"] = new SelectList(_context.GroupRecruitments, "Id", "Id", recruitment.GroupRecruitmentId);
            return View(recruitment);
        }

        // POST: Admin/Recruitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecruitmentId,JobTitle,JobDescription,CompanyName,CompanyAddress,Phone,Email,Salary,GroupRecruitmentId,Date,Status")] Recruitment recruitment)
        {
            if (id != recruitment.RecruitmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recruitment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecruitmentExists(recruitment.RecruitmentId))
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
            ViewData["GroupRecruitmentId"] = new SelectList(_context.GroupRecruitments, "Id", "Id", recruitment.GroupRecruitmentId);
            return View(recruitment);
        }

        // GET: Admin/Recruitments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitments
                .Include(r => r.GroupRecruitment)
                .FirstOrDefaultAsync(m => m.RecruitmentId == id);
            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // POST: Admin/Recruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recruitment = await _context.Recruitments.FindAsync(id);
            if (recruitment != null)
            {
                _context.Recruitments.Remove(recruitment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitmentExists(int id)
        {
            return _context.Recruitments.Any(e => e.RecruitmentId == id);
        }
    }
}
