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
			var parentCategories = _context.Categories
				.OrderByDescending(c => c.Id)
				.Where(c => c.ParentId == null)
				.ToList();
			var categories = _context.Categories
				.OrderByDescending(c => c.Id)
				.Where(c => c.ParentId != null)
				.ToList();
			var pagesL1 = _context.Pages
				.Where(l1 =>
					l1.Level != null &&
					l1.Level.Length == 5 &&
					l1.Active == 1 &&
					l1.Position == 1)
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
						l2.Position == 1
					),

					// Có cấp 3
					HasChildL3 = _context.Pages.Any(l3 =>
						l3.Level != null &&
						l3.Level.Length == 15 &&
						l3.Level.StartsWith(l1.Level) &&
						l3.Active == 1 &&
						l3.Position == 1
					)
				})
				.ToList();

			var pagesL2 = _context.Pages
				.OrderBy(x => x.Ord)
				.Where(x => x.Level != null 
					&& x.Level.Length == 10 
					&& x.Active == 1
					&& x.Position == 1)
				.ToList();

			var pagesL3 = _context.Pages
				.OrderBy(x => x.Ord)
				.Where(x => x.Level != null 
					&& x.Level.Length == 15 
					&& x.Active == 1
					&& x.Position == 1)
				.ToList();

			var pageSanPham = _context.Pages
				.Where(x => x.Position == 1 && x.Active == 1)
				.FirstOrDefault(x => x.Name.Trim().ToLower() == "sản phẩm");

            var config = _context.Configs.FirstOrDefault();
			var logo = _context.Advertises
				.OrderByDescending(x => x.Id)
				.Where(x => x.Position == 6)
				.FirstOrDefault() ?? new Advertise();

            if (config == null)
            {
                config = new Config
                {
                    MailSmtp = string.Empty,
                    MailInfo = string.Empty,
                    MailNoreply = string.Empty,
                    MailPassword = string.Empty,
                    PlaceHead = string.Empty,
                    PlaceBody = string.Empty,
                    GoogleId = string.Empty,
                    Contact = string.Empty,
                    Copyright = string.Empty,
                    Title = string.Empty,
                    Description = string.Empty,
                    Keyword = string.Empty,
                    Lang = string.Empty,
                    HotLine = string.Empty,
                    YoutubeLink = string.Empty,
                    PicasaLink = string.Empty,
                    FlickrLink = string.Empty,
                    SocialLink1 = string.Empty,
                    SocialLink2 = string.Empty,
                    SocialLink3 = string.Empty,
                    SocialLink4 = string.Empty,
                    SocialLink5 = string.Empty,
                    SocialLink6 = string.Empty,
                    SocialLink7 = string.Empty,
                    SocialLink8 = string.Empty,
                    SocialLink9 = string.Empty
                };
            }

			ViewBag.Logo = logo;
			ViewBag.Config = config;
            ViewBag.Categories = categories;
			ViewBag.ParentCategories = parentCategories;
			ViewBag.PagesL1 = pagesL1;
			ViewBag.PagesL2 = pagesL2;
			ViewBag.PagesL3 = pagesL3;
			ViewBag.PageTinTucLevel = pageSanPham?.Level;
			return View("Default");
		}
	}
}
