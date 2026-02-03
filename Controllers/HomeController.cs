using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MyShop.Models;
using System.Diagnostics;
using System.Security.Claims;

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

        [Route("/")]
        [Route("/trang-chu")]
        [Route("/trang-chu/{slug}")]
        public IActionResult Index(string slug = "")
        {
            var categories = _context.Categories
                //.Include(c => c.Products)
                .OrderByDescending(c => c.Id)
                .Where(c => c.Products.Any(p => p.Active == 1))
                .ToList();
            var products = _context.Products
                .Include(x => x.Category)
                .Where(p => p.Active == 1)
                .ToList();
            var productsFilter = new List<Product>();
            if (!string.IsNullOrEmpty(slug))
            {
                productsFilter = products.Where(x => x.Category.Tag == slug).ToList();
            }
            else
            {
                productsFilter = products;
            }
            var slides = _context.Advertises
                .Where(x => x.Position == 2 && x.Active)
                .OrderBy(x => x.Ord)
                .ToList();
            //var news = _context.News
            //    .OrderByDescending(x => x.Id)
            //    .Where(x => x.Status == 1)
            //    .ToList();

            //ViewBag.News = news;
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.ProductsFilter = productsFilter;
            ViewBag.Slides = slides;
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

        [Route("dang-nhap")]
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            string passmd5 = "";
            passmd5 = Cipher.GenerateMD5(password);
            var acc = _context.Users.SingleOrDefault(x => x.Username == userName && x.Password == passmd5 && x.Active == 1) ?? new User();
            if (acc != null)
            {
                HttpContext.Session.SetString("username", acc.Name);
                HttpContext.Session.SetString("accountId", acc.Id.ToString());

                return RedirectToAction("Index");
            }
            ViewBag.error = "<p class='alert alert-danger'>Email or password is incorrect!</p>";
            ViewBag.UserName = userName;
            ViewBag.password = password;
            return View();
        }

        [Route("dang-ky")]
        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [Route("dang-ky")]
		[HttpPost]
		public async Task<IActionResult> Register(string name = "", string userName = "", string password = "", string confirmPassword = "")
		{
            var acc = new User();
            acc.Name = name;
            acc.Username = userName;
            acc.Password = password;
			if (_context.Users.Any(x => x.Username.ToLower() == acc.Username.ToLower().Trim()))
			{
				ViewBag.ErrorUserName = "Tên tài khoản đã tồn tại!";
			}
			if (confirmPassword.Equals(acc.Password))
			{
				acc.Password = Cipher.GenerateMD5(acc.Password);
			}
			else
			{
				ViewBag.ErrorConfirmPassword = "Nhập mật khẩu chưa khớp!";
			}
			if (!string.IsNullOrEmpty(ViewBag.ErrorUserName) || !string.IsNullOrEmpty(ViewBag.ErrorConfirmPassword))
			{
				ViewBag.UserName = userName;
				ViewBag.Name = name;
				return View(acc);
			}
			_context.Add(acc);
			await _context.SaveChangesAsync();
			return Redirect("/dang-nhap");
			
		}



		[Route("dang-xuat")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
