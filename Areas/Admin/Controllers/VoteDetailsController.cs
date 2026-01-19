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
    public class VoteDetailsController : Controller
    {
        private readonly DbMyShopContext _context;

        public VoteDetailsController(DbMyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/VoteDetails
        public async Task<IActionResult> Index()
        {
            var dbMyShopContext = _context.VoteDetails.Include(v => v.News).Include(v => v.Vote);
            return View(await dbMyShopContext.ToListAsync());
        }

        // GET: Admin/VoteDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voteDetail = await _context.VoteDetails
                .Include(v => v.News)
                .Include(v => v.Vote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voteDetail == null)
            {
                return NotFound();
            }

            return View(voteDetail);
        }

        // GET: Admin/VoteDetails/Create
        public IActionResult Create()
        {
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id");
            ViewData["VoteId"] = new SelectList(_context.Votes, "Id", "Id");
            return View();
        }

        // POST: Admin/VoteDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Point,Ip,Date,VoteId,NewsId")] VoteDetail voteDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voteDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", voteDetail.NewsId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "Id", "Id", voteDetail.VoteId);
            return View(voteDetail);
        }

        // GET: Admin/VoteDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voteDetail = await _context.VoteDetails.FindAsync(id);
            if (voteDetail == null)
            {
                return NotFound();
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", voteDetail.NewsId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "Id", "Id", voteDetail.VoteId);
            return View(voteDetail);
        }

        // POST: Admin/VoteDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Point,Ip,Date,VoteId,NewsId")] VoteDetail voteDetail)
        {
            if (id != voteDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voteDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteDetailExists(voteDetail.Id))
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
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "Id", voteDetail.NewsId);
            ViewData["VoteId"] = new SelectList(_context.Votes, "Id", "Id", voteDetail.VoteId);
            return View(voteDetail);
        }

        // GET: Admin/VoteDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voteDetail = await _context.VoteDetails
                .Include(v => v.News)
                .Include(v => v.Vote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voteDetail == null)
            {
                return NotFound();
            }

            return View(voteDetail);
        }

        // POST: Admin/VoteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var voteDetail = await _context.VoteDetails.FindAsync(id);
            if (voteDetail != null)
            {
                _context.VoteDetails.Remove(voteDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteDetailExists(long id)
        {
            return _context.VoteDetails.Any(e => e.Id == id);
        }
    }
}
