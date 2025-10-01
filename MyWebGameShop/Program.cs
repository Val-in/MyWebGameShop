using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Middleware;
using MyWebGameShop.Services.Implementations;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ----- DB -----
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(conn));

        // ----- MVC -----
        builder.Services.AddControllersWithViews();

        // ----- Auth (Cookies) -----
        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = "/Account/Login";
                o.AccessDeniedPath = "/Account/AccessDenied";
            });

        // ----- DI -----
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IFeedbackService, FeedbackService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddScoped<ILogService, LogService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IRecommendationService, RecommendationService>();

        // ----- AutoMapper -----
        builder.Services.AddAutoMapper(typeof(MyWebGameShop.MappingProfile.GameProfile).Assembly);

        // ----- Session & HttpContext -----
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(o =>
        {
            o.IdleTimeout = TimeSpan.FromMinutes(30);
            o.Cookie.HttpOnly = true;
            o.Cookie.IsEssential = true;
        });
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // ИСПРАВЛЕН ПОРЯДОК MIDDLEWARE!
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Session должна быть до Authentication
        app.UseSession(); 

        app.UseAuthentication();
        app.UseAuthorization();

        // LoggingMiddleware лучше поставить после аутентификации
        app.UseMiddleware<LoggingMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}