using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Controllers
{
	public class VieclamController : Controller
	{
		private readonly DbMyShopContext _context;
        public VieclamController(DbMyShopContext context)
        {
            _context = context;
        }
        [Route("viec-lam")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("viec-lam-chi-tiet/{id:int}")]
		public IActionResult Chitiet(int id = 0, string s = "")
		{
			var vieclam = _context.Recruitments.FirstOrDefault(x => x.RecruitmentId == id) ?? new Recruitment();
			if (vieclam == null)
			{
				return RedirectToAction("Index");
			}
			var listVieclam = _context.News
				.OrderByDescending(x => x.Id)
				.Where(x => x.Active == 1)
				.Skip(0)
				.Take(3)
				.ToList();

			ViewBag.ListVieclam = listVieclam;
			ViewBag.Search = s;
			return View("viec-lam-chi-tiet", vieclam);
		}
	}
}
