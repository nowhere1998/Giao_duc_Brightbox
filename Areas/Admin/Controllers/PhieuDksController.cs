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
    public class PhieuDksController : Controller
    {
        private readonly DbMyShopContext _context;

        public PhieuDksController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/PhieuDks
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhieuDks.ToListAsync());
        }

        // GET: Admin/PhieuDks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuDk = await _context.PhieuDks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuDk == null)
            {
                return NotFound();
            }

            return View(phieuDk);
        }

        // GET: Admin/PhieuDks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PhieuDks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hoten,Ngaysinh,Gioitinh,SoDt,Email,Diachi,Trinhdo,Tentruong,Cmnd,Ngaycap,Noicap,NganhDk,PhuongthucTs,Tongdiem,Camket,NgayDk,Ghichu")] PhieuDk phieuDk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuDk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phieuDk);
        }

        // GET: Admin/PhieuDks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuDk = await _context.PhieuDks.FindAsync(id);
            if (phieuDk == null)
            {
                return NotFound();
            }
            return View(phieuDk);
        }

        // POST: Admin/PhieuDks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hoten,Ngaysinh,Gioitinh,SoDt,Email,Diachi,Trinhdo,Tentruong,Cmnd,Ngaycap,Noicap,NganhDk,PhuongthucTs,Tongdiem,Camket,NgayDk,Ghichu")] PhieuDk phieuDk)
        {
            if (id != phieuDk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuDk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuDkExists(phieuDk.Id))
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
            return View(phieuDk);
        }

        // GET: Admin/PhieuDks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuDk = await _context.PhieuDks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuDk == null)
            {
                return NotFound();
            }

            return View(phieuDk);
        }

        // POST: Admin/PhieuDks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuDk = await _context.PhieuDks.FindAsync(id);
            if (phieuDk != null)
            {
                _context.PhieuDks.Remove(phieuDk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuDkExists(int id)
        {
            return _context.PhieuDks.Any(e => e.Id == id);
        }
    }
}
