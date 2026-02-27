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

		/*[Route("viec-lam-chi-tiet/{slug}")]
		public IActionResult Chitiet(string slug = "", string s = "")
		{
			var tintuc = _context.Recruitments.FirstOrDefault(x => x.Tag == slug) ?? new News();
			var news = _context.News
				.OrderByDescending(x => x.Id)
				.Where(x => x.Active == 1)
				.Skip(0)
				.Take(3)
				.ToList();

			ViewBag.News = news;
			ViewBag.Search = s;
			return View("viec-lam-chi-tiet", tintuc);
		}*/
	}
}
