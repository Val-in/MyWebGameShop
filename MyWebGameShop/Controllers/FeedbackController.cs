using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;

namespace MyWebGameShop.Controllers;

public class FeedbackController : Controller
{
    /// <summary>
    ///  Метод, возвращающий страницу с отзывами
    /// </summary>
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
  
    /// <summary>
    /// Метод для Ajax-запросов. Ajax предоставляет ещё один способ передачи данных на сервер без обновления страницы целиком. 
    /// </summary>
    [HttpPost]
    public IActionResult Add(Feedback feedback)
    {
        return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}