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
			
			return View("Default");
		}
	}
}
