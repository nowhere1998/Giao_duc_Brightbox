using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers.Components
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly DbMyShopContext _context;

        public HeaderViewComponent(DbMyShopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var config = _context.Configs.FirstOrDefault();

            if (config == null)
            {
                config = new Config
                {
                    MailSmtp = string.Empty,
                    MailInfo = string.Empty,
                    MailNoreply = string.Empty,
                    MailPassword = string.Empty,
                    PlaceHead = string.Empty,
                    PlaceBody = string.Empty,
                    GoogleId = string.Empty,
                    Contact = string.Empty,
                    Copyright = string.Empty,
                    Title = string.Empty,
                    Description = string.Empty,
                    Keyword = string.Empty,
                    Lang = string.Empty,
                    HotLine = string.Empty,
                    YoutubeLink = string.Empty,
                    PicasaLink = string.Empty,
                    FlickrLink = string.Empty,
                    SocialLink1 = string.Empty,
                    SocialLink2 = string.Empty,
                    SocialLink3 = string.Empty,
                    SocialLink4 = string.Empty,
                    SocialLink5 = string.Empty,
                    SocialLink6 = string.Empty,
                    SocialLink7 = string.Empty,
                    SocialLink8 = string.Empty,
                    SocialLink9 = string.Empty
                };
            }

            return View("Default", config);
        }
    }
}
