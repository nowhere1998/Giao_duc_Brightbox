using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class KhoahocController : Controller
	{
		[Route("khoa-hoc")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("khoa-hoc/chi-tiet")]
		public IActionResult Chitiet()
		{
			return View("khoa-hoc-chi-tiet");
		}
	}
}
