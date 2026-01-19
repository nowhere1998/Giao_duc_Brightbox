using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System.Security.Claims;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class AdminHomeController : Controller
    {
        private readonly DbMyShopContext _context;
        public AdminHomeController(DbMyShopContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [Route("login")]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //trong AdminHomeController
       [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.error = "Tài khoản hoặc mật khẩu không được để trống";
                ViewBag.userName = userName;
                return View();
            }
            string passmd5 = Cipher.GenerateMD5(password).ToLower();
            var acc = _context.Users.SingleOrDefault(x => x.Username.ToLower() == userName.ToLower() && x.Password.ToLower() == passmd5);
            if (acc == null)
            {
                ViewBag.error = "Tài khoản hoặc mật khẩu không đúng";
                ViewBag.userName = userName;
                return View();
            }
            if (acc.Admin != 1)
            {
                ViewBag.error = "Tài khoản không có quyền truy cập";
                ViewBag.userName = userName;
                return View();
            }

            var identity = new ClaimsIdentity(new[] {
            new Claim("UserId", acc.Id.ToString()),
            new Claim("UserName", acc.Username),
        }, "MyShopSecurityScheme");

            var principal = new ClaimsPrincipal(identity);

            // AWAIT để cookie được ghi hoàn chỉnh trước khi redirect
            await HttpContext.SignInAsync("MyShopSecurityScheme", principal);

            return RedirectToAction("Index");
        }



        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("MyShopSecurityScheme");
            return RedirectToAction("login");
        }
    }
}
