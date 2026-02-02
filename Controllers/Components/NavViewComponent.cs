using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers.Components
{
	public class NavViewComponent : ViewComponent
	{
		private readonly DbMyShopContext _context;
		public NavViewComponent(DbMyShopContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{

			//var parentCategories = _context.Categories
			//	.OrderByDescending(c => c.Id)
			//	.Where(c => c.ParentId == null)
			//	.ToList();
			//var categories = _context.Categories
			//	.OrderByDescending(c => c.Id)
			//	.Where(c => c.ParentId != null)
			//	.ToList();

			var pagesL1 = _context.Pages
				.Where(l1 =>
					l1.Level != null &&
					l1.Level.Length == 5 &&
					l1.Active == 1 &&
					(l1.Position == 1 || l1.Position == 6))
				.OrderBy(l1 => l1.Ord)
				.Select(l1 => new PageL1
				{
					Page = l1,

					// Có cấp 2
					HasChild = _context.Pages.Any(l2 =>
						l2.Level != null &&
						l2.Level.Length == 10 &&
						l2.Level.StartsWith(l1.Level) &&
						l2.Active == 1 &&
						(l2.Position == 1 || l2.Position == 6)
					),

				})
				.ToList();

			var pagesL2 = _context.Pages
				.OrderBy(x => x.Ord)
				.Where(x => x.Level != null
					&& x.Level.Length == 10
					&& x.Active == 1
					&& (x.Position == 1 || x.Position == 6))
				.ToList();

			var config = _context.Configs.FirstOrDefault() ?? new Config();
			var logo = _context.Advertises
				.OrderBy(x => x.Ord)
				.Where(x => x.Position == 6 && x.Active == true)
				.FirstOrDefault() ?? new Advertise();

			var topBanner = _context.Advertises
				.OrderBy(x => x.Ord)
				.Where(x => x.Position == 7 && x.Active == true)
				.FirstOrDefault() ?? new Advertise();

			ViewBag.TopBanner = topBanner;
			ViewBag.Logo = logo;
			ViewBag.Config = config;
			//ViewBag.Categories = categories;
			//ViewBag.ParentCategories = parentCategories;
			ViewBag.PagesL1 = pagesL1;
			ViewBag.PagesL2 = pagesL2;
			return View("Default");
		}
	}
}
