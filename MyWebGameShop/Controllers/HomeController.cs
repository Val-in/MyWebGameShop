using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels.ErrorViewModel;

namespace MyWebGameShop.Controllers
{
    /// <summary>
    /// Контроллер в ASP.NET, т.е. класс, который обрабатывает HTTP-запросы от клиента и возвращает ответы (HTML, JSON и т.д.).
    /// Контроллеры разделены по зонам приложения (Account, Home, Products и т.д.), а сервисы — по функциональной логике (Cart, Order, User и т.п.).
    /// Например: HomeController может просто рендерить страницы и вообще не вызывать сервисов. HomeController только для общих страниц: главная, privacy, статические страницы.
    /// AccountController может работать с UserService для логина/регистрации.
    /// ProductsController — с ProductService.
    /// </summary>
    public class HomeController : Controller 
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger; //логирует конкретные события (например, успешная регистрация пользователя, ошибка в методе).
 
        // Также добавим инициализацию в конструктор
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is Index");
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
