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
    public class GroupRecruitmentsController : Controller
    {
        private readonly DbMyShopContext _context;

        public GroupRecruitmentsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/GroupRecruitments
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupRecruitments.ToListAsync());
        }

        // GET: Admin/GroupRecruitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRecruitment = await _context.GroupRecruitments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupRecruitment == null)
            {
                return NotFound();
            }

            return View(groupRecruitment);
        }

        // GET: Admin/GroupRecruitments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GroupRecruitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tag,Status")] GroupRecruitment groupRecruitment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupRecruitment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupRecruitment);
        }

        // GET: Admin/GroupRecruitments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRecruitment = await _context.GroupRecruitments.FindAsync(id);
            if (groupRecruitment == null)
            {
                return NotFound();
            }
            return View(groupRecruitment);
        }

        // POST: Admin/GroupRecruitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tag,Status")] GroupRecruitment groupRecruitment)
        {
            if (id != groupRecruitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupRecruitment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupRecruitmentExists(groupRecruitment.Id))
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
            return View(groupRecruitment);
        }

        // GET: Admin/GroupRecruitments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRecruitment = await _context.GroupRecruitments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupRecruitment == null)
            {
                return NotFound();
            }

            return View(groupRecruitment);
        }

        // POST: Admin/GroupRecruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupRecruitment = await _context.GroupRecruitments.FindAsync(id);
            if (groupRecruitment != null)
            {
                _context.GroupRecruitments.Remove(groupRecruitment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupRecruitmentExists(int id)
        {
            return _context.GroupRecruitments.Any(e => e.Id == id);
        }
    }
}
