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
                .OrderBy(c => c.Ord)
                .Where(c => c.Active == 1 )
                .ToList();
			var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Enrollments)
                .Include(p => p.CommentPros)
                .Include(p => p.Faculty)
	            .Where(p => p.Active == 1)
	            .Select(p => new ProductViewModel
				{
		            Product = p,
		            AvgRate = p.CommentPros.Any(x => x.Rate != null)
			            ? p.CommentPros.Average(x => x.Rate ?? 0)
			            : 0,
					TotalEnroll = p.Enrollments.Count()
				})
	            .ToList();
			var productsFilter = new List<ProductViewModel>();
            if (!string.IsNullOrEmpty(slug))
            {
                productsFilter = products
                    .Where(x => x.Product.Category.Tag == slug)
                    .Skip(0)
                    .Take(3)
                    .ToList();
            }
            else
            {
                productsFilter = products
                    .Skip(0)
					.Take(3)
					.ToList(); 
            }
            var slides = _context.Advertises
                .Where(x => x.Position == 2 && x.Active)
                .OrderBy(x => x.Ord)
                .ToList();
            var dn = _context.Advertises
                .Where(x => x.Position == 8  && x.Active)
                .OrderBy(x => x.Ord)
                .ToList();
			var tt = _context.Advertises
				.Where(x => x.Position == 9 && x.Active)
				.OrderBy(x => x.Ord)
				.ToList();
            var thongke = _context.Pages
                .Where(x => x.Position == 7 && x.Active == 1)
                .FirstOrDefault() ?? new Page();
			var comments = _context.CommentPros
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .OrderByDescending(x => x.Id)
                .Where(x => x.Active == 1)
                .Skip(0)
                .Take(3)
                .ToList();

			ViewBag.Comments = comments;
			ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.ProductsFilter = productsFilter;
            ViewBag.Slides = slides;
            ViewBag.Slug = slug;
            ViewBag.Doanhnghiep = dn;
            ViewBag.Truyenthong = tt;
            ViewBag.Thongke = thongke;
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
        public async Task<IActionResult> Login(string userName = "", string password = "")
        {
            string passmd5 = "";
            passmd5 = Cipher.GenerateMD5(password);
            var acc = _context.Customers.SingleOrDefault(x => x.UserName == userName && x.Password == passmd5 && x.Active == 1);
            if (acc != null)
            {
                HttpContext.Session.SetString("username", acc.UserName);
                HttpContext.Session.SetString("accountId", acc.Id.ToString());

                return RedirectToAction("Index");
            }
			TempData["Error"] = "Tên tài khoản hoặc mật khẩu không đúng!";
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
            var acc = new Customer();
            acc.Name = name;
            acc.UserName = userName;
            acc.Password = password;
            bool hasError = false;
            string error = "";
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(name))
            {
				hasError = true;
				error = "Tên không được để rỗng!";
			}
			if (_context.Customers.Any(x => x.UserName.ToLower() == acc.UserName.ToLower().Trim()))
			{
                hasError = true;
				error = "Tên tài khoản đã tồn tại!";
			}
			if (confirmPassword.Equals(acc.Password))
			{
				acc.Password = Cipher.GenerateMD5(acc.Password);
			}
			else
			{
				hasError = true;
				error = "Nhập password chưa khớp!";
			}
			if (hasError)
			{
				TempData["Error"] = error;
				ViewBag.UserName = userName;
				ViewBag.Name = name;
				return View(acc);
			}
            acc.Active = 1;
			_context.Customers.Add(acc);
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
