using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class VieclamController : Controller
	{
		[Route("viec-lam")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
