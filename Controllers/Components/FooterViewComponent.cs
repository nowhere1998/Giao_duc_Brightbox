using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly DbMyShopContext _context;

        public FooterViewComponent(DbMyShopContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            return View("Default");
        }
    }
}
