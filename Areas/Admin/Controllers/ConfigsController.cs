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
    public class ConfigsController : Controller
    {
        private readonly DbMyShopContext _context;

        public ConfigsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Configs
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Configs.ToListAsync());
        //}

        // GET: Admin/Configs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Configs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

        // GET: Admin/Configs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Configs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MailSmtp,MailPort,MailInfo,MailNoreply,MailPassword,PlaceHead,PlaceBody,GoogleId,Contact,Copyright,Title,Description,Keyword,Lang,HotLine,YoutubeLink,PicasaLink,FlickrLink,SocialLink1,SocialLink2,SocialLink3,SocialLink4,SocialLink5,SocialLink6,SocialLink7,SocialLink8,SocialLink9")] Config config)
        {
            if (ModelState.IsValid)
            {
                _context.Add(config);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(config);
        }

        // GET: Admin/Configs/Edit/5
        public async Task<IActionResult> Edit()
        {
            var config = await _context.Configs.FirstOrDefaultAsync();

            // Nếu chưa có thì tạo mới để người dùng nhập
            if (config == null)
            {
                config = new Config();
            }

            return View(config);
        }

        // POST: Admin/Configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Config config)
        {
            if (id != config.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // lấy mật khẩu cũ từ DB
                    var oldPassword = await _context.Configs
                        .Where(x => x.Id == config.Id)
                        .Select(x => x.MailPassword)
                        .FirstOrDefaultAsync();

                    // 👉 xử lý mật khẩu
                    if (string.IsNullOrWhiteSpace(config.MailPassword))
                    {
                        // không nhập → giữ mật khẩu cũ
                        config.MailPassword = oldPassword;
                    }
                    else
                    {
                        // có nhập → hash mật khẩu mới
                        config.MailPassword = Cipher.GenerateMD5(config.MailPassword);
                    }
                    _context.Update(config);
                    await _context.SaveChangesAsync();

                    // GIỮ NGUYÊN TRANG EDIT
                    return RedirectToAction(nameof(Edit), new { id = config.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Configs.Any(c => c.Id == config.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(config);
        }


        // GET: Admin/Configs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Configs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

        // POST: Admin/Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var config = await _context.Configs.FindAsync(id);
            if (config != null)
            {
                _context.Configs.Remove(config);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}
