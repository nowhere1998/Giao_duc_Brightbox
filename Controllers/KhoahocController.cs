using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyShop.Controllers
{
	public class KhoahocController : Controller
	{
		private readonly DbMyShopContext _context;

		public KhoahocController(DbMyShopContext context)
		{
			_context = context;
		}
		[Route("khoa-hoc")]
		[Route("khoa-hoc/page/{page:int}")]
		[Route("khoa-hoc/{slug}")]
		[Route("khoa-hoc/{slug}/page/{page:int}")]
		public IActionResult Index(string? slug = "", int page = 1, string search = "")
		{
			int pageSize = 6;
			int totalItems = 0;
			var products = _context.Products
				.Include(p => p.Category)
				.Include(p => p.Enrollments)
				.Include(p => p.CommentPros)
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
			var category = _context.Categories.FirstOrDefault(x => x.Tag == slug);

			// 🔹 Lọc theo danh mục (slug)
			if (!string.IsNullOrEmpty(slug))
			{
				category = _context.Categories.FirstOrDefault(x => x.Tag == slug);
				if (category != null)
				{
					products = products.Where(p => p.Product.CategoryId == category.Id).ToList();
				}
			}

			//lọc theo tên
			if (!string.IsNullOrEmpty(search))
			{
				products = products
					.Where(x =>
						x.Product.Name.Trim().ToLower().Contains(search.Trim().ToLower())
						|| x.Product.Tag.Trim().ToLower().Contains(search.Trim().ToLower())
						)
					.ToList();
			}

			// Tổng số sản phẩm sau khi lọc
			totalItems = totalItems > 0 ? totalItems : products.Count();
			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			products = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var categories = _context.Categories
				.Include(x => x.Products)
				.Where(x => x.Active == 1)
				.OrderBy(x => x.Ord)
				.ToList();

			//Lấy page danh mục
			var pagesL2 = _context.Pages
				.OrderBy(x => x.Ord)
				.Where(x => x.Level != null && x.Level.Length == 10 && x.Active == 1)
				.ToList();

			ViewBag.PagesL2 = pagesL2;
			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;
			ViewBag.Categories = categories;
			ViewBag.Slug = slug;
			ViewBag.Search = search;
			return View(products);
		}

		[Route("khoa-hoc-chi-tiet/{slug}")]
		[HttpGet]
		public IActionResult Chitiet(string slug = "")
		{
			var product = _context.Products
				.Include(p => p.Category)
				.Include(p => p.Enrollments)
				.Include(p => p.CommentPros)
				.Where(p => p.Active == 1 && p.Tag == slug)
				.Select(p => new ProductViewModel
				{
					Product = p,
					AvgRate = p.CommentPros.Any(x => x.Rate != null)
						? p.CommentPros.Average(x => x.Rate ?? 0)
						: 0,
					TotalEnroll = p.Enrollments.Count()
				})
				.FirstOrDefault() ?? new ProductViewModel();
			var comments = _context.CommentPros
				.Include(x => x.Product)
				.Include(x => x.Customer)
				.OrderByDescending(x => x.Id)
				.Where(x => x.Active == 1 && x.Product.Tag == slug)
				.ToList();

			ViewBag.Comments = comments;
			return View("khoa-hoc-chi-tiet", product);
		}

		[HttpPost]
		public IActionResult CreateComment(CommentPro model)
		{
			// 🔒 CHƯA ĐĂNG NHẬP
			//if (HttpContext.Session.GetString("username") == null)
			//{
			//	TempData["Error"] = "Bạn cần đăng nhập để gửi đánh giá";
			//	return Redirect(Request.Headers["Referer"].ToString());
			//}

			// ❌ CHƯA CHỌN SAO
			if (model.Rate == null || model.Rate < 1 || model.Rate > 5)
			{
				TempData["Error"] = "Vui lòng chọn số sao đánh giá";
				return RedirectToAction("Chitiet");
			}

			// ❌ NỘI DUNG RỖNG
			if (string.IsNullOrWhiteSpace(model.Comment1))
			{
				TempData["Error"] = "Nội dung đánh giá không được để trống";
				return RedirectToAction("Chitiet");
			}

			// ❌ TRÙNG ĐÁNH GIÁ (1 KHÁCH / 1 KHÓA)
			bool isExist = _context.CommentPros.Any(x =>
				x.CustomerId == model.CustomerId &&
				x.ProductId == model.ProductId
			);

			if (isExist)
			{
				TempData["Error"] = "Bạn đã đánh giá khóa học này rồi";
				return RedirectToAction("Chitiet");
			}

			// ✅ LƯU COMMENT
			var comment = new CommentPro
			{
				CustomerId = model.CustomerId,
				ProductId = model.ProductId,
				Rate = model.Rate,
				Comment1 = model.Comment1,
				Date = DateTime.Now,
				Active = 0 
			};

			_context.CommentPros.Add(comment);
			_context.SaveChanges();

			TempData["Success"] = "Cảm ơn bạn! Đánh giá của bạn đang chờ duyệt ❤️";
			return Redirect(Request.Headers["Referer"].ToString());
		}
	}
}
