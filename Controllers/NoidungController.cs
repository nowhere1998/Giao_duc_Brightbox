using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class NoidungController : Controller
	{
		[Route("noi-dung/{slug}")]
		public IActionResult Index(string slug = "")
		{
			return View();
		}
	}
}
