using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class DichvuController : Controller
	{
		[Route("dich-vu-lap-dat")]
		public IActionResult DichvuLapdat()
		{
			return View("dich-vu-lap-dat");
		}

		[Route("dich-vu-cho-thue")]
		public IActionResult DichvuChoThue()
		{
			return View("dich-vu-cho-thue");
		}

		[Route("quy-dinh-bao-hanh")]
		public IActionResult QuydinhBaohanh()
		{
			return View("quy-dinh-bao-hanh");
		}
	}
}
