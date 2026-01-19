using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers.Components
{
	public class ZaloViewComponent : ViewComponent
	{
		private readonly DbMyShopContext _context;
		public ZaloViewComponent(DbMyShopContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{

			return View("Default");
		}
	}
}
