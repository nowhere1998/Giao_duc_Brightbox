using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
	public class TintucController : Controller
	{
		private readonly DbMyShopContext _context;
        public TintucController(DbMyShopContext context)
        {
            _context = context;
        }
        [Route("tin-tuc")]
        public IActionResult Index(int page = 1, string s = "")
        {
            int pageSize = 5;

            var query = _context.News
                .Where(x => x.Active == 1 && x.Name.ToLower().Contains(s.ToLower().Trim()))
                .OrderByDescending(x => x.Id);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var news = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
			ViewBag.Search = s;

			return View(news);
        }

        [Route("tin-tuc-chi-tiet/{slug}")]
        public IActionResult Chitiet(string slug = "", string s = "")
        {
            var tintuc = _context.News.FirstOrDefault(x => x.Tag == slug) ?? new News();
            var news = _context.News
                .OrderByDescending (x => x.Id)  
                .Where(x => x.Active == 1)
                .Skip(0)
                .Take(3)
                .ToList();

            ViewBag.News = news;
            ViewBag.Search = s;
            return View("tin-tuc-chi-tiet", tintuc);
        }
    }
}
