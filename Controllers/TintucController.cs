using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
	public class TintucController : Controller
	{
		[Route("tin-tuc")]
		public IActionResult Index()
		{
			return View();
		}

        [Route("tin-tuc/chi-tiet")]
        public IActionResult Chitiet()
        {
            return View("tin-tuc-chi-tiet");
        }
    }
}
