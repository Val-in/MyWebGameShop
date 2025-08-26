using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Middleware;
using MyWebGameShop.Services.Implementations;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Получаем строку подключения
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // Добавляем DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connection)); //метод AddDbContext, его тип AddDbContext

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IUserService, UserService>();

            /*(Если пользователь сделал запрос → сервис создаётся → работает пока идёт запрос → потом уничтожается).
            Есть ещё AddSingleton (один объект на всё приложение) и AddTransient (новый объект при каждом обращении).
            – Если где-то в коде попросят IUserService, то DI даст экземпляр класса UserService.
            Аналогично для ILogService → LogService, ICartService → CartService, и т.д.
            AddScoped<IService, Service>() → настраивает внедрение зависимостей: "когда просят интерфейс, отдай этот класс".
            Если в контроллере два раза вызывается Transient → два разных экземпляра.
            Если два раза вызывается Scoped → один экземпляр для всего запроса.*/

            var app = builder.Build();

            // Configure the HTTP request pipeline (конвейер обработки HTTP-запросов).
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                /*HSTS = HTTP Strict Transport Security.
                Заставляет браузер всегда использовать HTTPS.*/
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            /*Если ввести в браузере /Products/List/5
                → controller = Products, action = List, id = 5.

                Если просто /
                → попадём в HomeController, метод Index().*/

            app.Run();
        }
    }
}
