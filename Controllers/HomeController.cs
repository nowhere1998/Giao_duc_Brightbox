using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System.Diagnostics;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbMyShopContext _context;

        public HomeController(ILogger<HomeController> logger, DbMyShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //var categories = _context.Categories
            //    .Include(c => c.Parent)
            //    .Include(c => c.Products)
            //    .OrderByDescending(c => c.Id)
            //    .Where(c => c.ParentId != null && c.Products != null)
            //    .Where(c => c.Products.Any(p => p.Status == "active"))
            //    .Skip(0)
            //    .Take(10)
            //    .ToList();
            //var products = _context.Products
            //    .Where(p => p.Status == "active")
            //    .ToList();
            //var banners = _context.Advertises
            //    .Where(x => x.Position == 1)
            //    .OrderBy(x => x.Ord)
            //    .ToList();
            //var news = _context.News
            //    .OrderByDescending(x => x.Id)
            //    .Where(x => x.Status == 1)
            //    .ToList();

            //ViewBag.News = news;
            //ViewBag.Categories = categories;
            //ViewBag.Products = products;
            //ViewBag.Banners = banners;
            return View();
        }

        public IActionResult Error(int? statusCode)
        {
            ViewBag.StatusCode = statusCode;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }
        
        [Route("dang-ky")]
        public IActionResult Register()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
