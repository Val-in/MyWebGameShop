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
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
