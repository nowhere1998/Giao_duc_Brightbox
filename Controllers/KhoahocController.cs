using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

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
		public IActionResult Index(long? minPrice, long? maxPrice, string? orderby, string? slug, int page = 1, string search = "")
		{
			int pageSize = 12;
			int totalItems = 0;
			var products = _context.Products
				.Where(p => p.Active == 1)
				.ToList();
			var category = _context.Categories.FirstOrDefault(x => x.Tag == slug);

			//lọc theo tên
			if (!string.IsNullOrEmpty(search))
			{
				products = products
					.Where(x =>
						x.Name.Trim().ToLower().Contains(search.Trim().ToLower())
						|| x.Tag.Trim().ToLower().Contains(search.Trim().ToLower())
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
				//.Where(x => x.Products.Any())
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
			ViewBag.Orderby = orderby;
			ViewBag.Search = search;
			return View(products);
		}

		[Route("khoa-hoc-chi-tiet")]
		public IActionResult Chitiet()
		{
			return View("khoa-hoc-chi-tiet");
		}
	}
}
