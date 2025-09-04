using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;
using MyWebGameShop.ViewModels.ErrorViewModel;

namespace MyWebGameShop.Controllers;

[Route("feedback")]
public class FeedbackController : Controller
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    /// <summary>
    /// Отображение формы для добавления отзыва
    /// </summary>
    [HttpGet]
    public IActionResult Add()
    {
        return View(new FeedBackViewModel());
    }

    /// <summary>
    /// Обработка отправки формы через POST (можно и через AJAX)
    /// Метод для Ajax-запросов. Ajax предоставляет ещё один способ передачи данных на сервер без обновления страницы целиком. 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add(FeedBackViewModel feedback)
    {
        if (!ModelState.IsValid)
        {
            return View(feedback);
        }

        await _feedbackService.AddFeedbackAsync(feedback);
        
        return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel 
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}