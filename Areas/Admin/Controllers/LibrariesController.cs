using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LibrariesController : Controller
    {
        private readonly DbMyShopContext _context;

        // ✅ Inject DbContext (KHÔNG new)
        public LibrariesController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Libraries
        public IActionResult Index(
    int page = 1,
    string GroupLibraryId = null,
    string newsTitle = null,
    string Active = null)
        {
            int pageSize = 10;

            var libraries = _context.Libraries
                .Include(x => x.GroupLibrary)
                .AsNoTracking()
                .AsQueryable();

            // Lọc GroupLibrary
            if (!string.IsNullOrEmpty(GroupLibraryId) &&
                int.TryParse(GroupLibraryId, out int groupId))
            {
                libraries = libraries.Where(x => x.GroupLibraryId == groupId);
            }

            // Lọc theo tên
            if (!string.IsNullOrEmpty(newsTitle))
            {
                libraries = libraries.Where(x => x.Name.Contains(newsTitle));
            }

            // Lọc Active
            if (!string.IsNullOrEmpty(Active) &&
                int.TryParse(Active, out int isActive))
            {
                libraries = libraries.Where(x => x.Active == isActive);
            }

            // Tổng bản ghi
            int totalItems = libraries.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Dữ liệu trang hiện tại
            var data = libraries
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // ===== ViewBag cho phân trang =====
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            // Giữ filter khi bấm trang
            ViewBag.newsTitle = newsTitle;
            ViewBag.GroupLibraryId = GroupLibraryId;
            ViewBag.Active = Active;

            // Dropdown
            ViewBag.GroupLibraries = _context.GroupLibraries.ToList();

            return View(data);
        }


        // GET: Admin/Libraries/Create
        public IActionResult Create()
        {
            ViewBag.GroupLibraries = _context.GroupLibraries.ToList();
            ViewBag.Members = _context.Members.ToList();
            return View();
        }

        // POST: Admin/Libraries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Library data)
        {
            data.Active ??= 0;

            // Giá trị mặc định
            data.Tag = "";
            data.Info = "";
            data.File = "";
            data.MemberId = 0;
            data.Lang = "";
            data.Priority = 0;
            _context.Libraries.Add(data);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Libraries/Edit/5
        public IActionResult Edit(int id)
        {
            var library = _context.Libraries.FirstOrDefault(x => x.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            ViewBag.GroupLibraries = _context.GroupLibraries.ToList();
            ViewBag.Members = _context.Members.ToList();

            return View(library);
        }

        // POST: Admin/Libraries/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Library data)
        {
            data.Active ??= 0;

            var dbLibrary = _context.Libraries.FirstOrDefault(x => x.Id == data.Id);
            if (dbLibrary == null)
            {
                return NotFound();
            }



            // Update field
            dbLibrary.Name = data.Name;
            dbLibrary.GroupLibraryId = data.GroupLibraryId;
            dbLibrary.Active = data.Active;
            dbLibrary.Tag = "";
            dbLibrary.Info = "";
            dbLibrary.File = "";
            dbLibrary.MemberId = 0;
            dbLibrary.Lang = "";
            dbLibrary.Priority = 0;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Libraries/Delete/5
        public IActionResult Delete(int id)
        {
            var library = _context.Libraries.FirstOrDefault(x => x.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            _context.Libraries.Remove(library);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
