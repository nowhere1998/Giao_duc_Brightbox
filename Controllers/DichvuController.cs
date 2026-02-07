using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class DichvuController : Controller
	{
		[Route("dich-vu")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
