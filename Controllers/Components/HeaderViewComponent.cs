using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly DbMyShopContext _context;

        public HeaderViewComponent(DbMyShopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var config = _context.Configs.FirstOrDefault() ?? new Config();

            return View("Default", config);
        }
    }
}
