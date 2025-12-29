using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyShop.middleware;
using MyShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbMyShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddDistributedMemoryCache();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(option =>
{
    option.Cookie.Name = "myShop.session";
    option.Cookie.HttpOnly = true;
    option.IdleTimeout = TimeSpan.FromHours(1);
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication("MyShopSecurityScheme").AddCookie("MyShopSecurityScheme", options =>
{
    options.AccessDeniedPath = new PathString("/Admin/");
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".MyShop.Security.Cookie",
        Path = "/",
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
    {

    };
    options.LoginPath = new PathString("/admin/login");
    options.ReturnUrlParameter = "redirectPath";
    options.SlidingExpiration = true;
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
//app.UseAuthentication();

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<BlockAuthPagesMiddleware>();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=AdminHome}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
