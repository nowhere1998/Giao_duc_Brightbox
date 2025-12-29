// Middlewares/BlockAuthPagesMiddleware.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MyShop.middleware
{
    public class BlockAuthPagesMiddleware
    {
        private readonly RequestDelegate _next;

        public BlockAuthPagesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            // Force no-cache for admin area so browser doesn't serve cached pages on Back
            if (path.StartsWithSegments("/admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "0";
            }

            // Nếu user đã auth nhưng cố vào /admin/login thì redirect về /admin
            if (context.User?.Identity != null &&
                context.User.Identity.IsAuthenticated &&
                path.StartsWithSegments("/admin/login", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.Redirect("/admin");
                return;
            }

            await _next(context);
        }
    }
}
