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
    public class TbSukiensController : Controller
    {
        private readonly DbMyShopContext _context;

        public TbSukiensController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/TbSukiens
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbSukiens.ToListAsync());
        }

        // GET: Admin/TbSukiens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSukien = await _context.TbSukiens
                .FirstOrDefaultAsync(m => m.MaSk == id);
            if (tbSukien == null)
            {
                return NotFound();
            }

            return View(tbSukien);
        }

        // GET: Admin/TbSukiens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TbSukiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSk,Tensukien,Noidung,NguonLink,Hienthi,Accid,Iviews,Metatitle,Metakeyword,Metadescription,Tag,Keyname")] TbSukien tbSukien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbSukien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbSukien);
        }

        // GET: Admin/TbSukiens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSukien = await _context.TbSukiens.FindAsync(id);
            if (tbSukien == null)
            {
                return NotFound();
            }
            return View(tbSukien);
        }

        // POST: Admin/TbSukiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSk,Tensukien,Noidung,NguonLink,Hienthi,Accid,Iviews,Metatitle,Metakeyword,Metadescription,Tag,Keyname")] TbSukien tbSukien)
        {
            if (id != tbSukien.MaSk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbSukien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbSukienExists(tbSukien.MaSk))
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
            return View(tbSukien);
        }

        // GET: Admin/TbSukiens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSukien = await _context.TbSukiens
                .FirstOrDefaultAsync(m => m.MaSk == id);
            if (tbSukien == null)
            {
                return NotFound();
            }

            return View(tbSukien);
        }

        // POST: Admin/TbSukiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbSukien = await _context.TbSukiens.FindAsync(id);
            if (tbSukien != null)
            {
                _context.TbSukiens.Remove(tbSukien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbSukienExists(int id)
        {
            return _context.TbSukiens.Any(e => e.MaSk == id);
        }
    }
}
