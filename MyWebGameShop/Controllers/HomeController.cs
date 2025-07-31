using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using System.Diagnostics;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers
{
    /// <summary>
    /// Контроллер в ASP.NET, т.е. класс, который обрабатывает HTTP-запросы от клиента и возвращает ответы (HTML, JSON и т.д.).
    /// </summary>
    public class HomeController : Controller //почему все контроллеры возвращают View??
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;
 
        // Также добавим инициализацию в конструктор
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var newUser = new User
            {
                Id = 2,
                UserName = "Katya",
                UserAgent = HttpContext.Request.Headers["User-Agent"],
                Login = "fgfg",
                Password = "Pass2",
                Email = "kk@aa.com",
                WalletBalance = 500
            };

            await _userService.AddUser(newUser);
            Console.WriteLine($"User with id {newUser.Id}, named {newUser.UserName} got balance: {newUser.WalletBalance}");

            return View();
        }
        
        public async Task <IActionResult> Users()
        {
            var users = await _userService.GetUsers();
      
            Console.WriteLine("See all blog users:");
            foreach (var user in users)
                Console.WriteLine($"User name {user.UserName}, Balance {user.WalletBalance}");
      
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Recommendaitons()
        {
            return View();
        }

        public IActionResult Subscriptions()
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
