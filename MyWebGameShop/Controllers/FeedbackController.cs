using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FeedBackViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            
            var entity = new Feedback();

            var vmProps = typeof(FeedBackViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entProps = typeof(Feedback).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                           .ToDictionary(p => p.Name, p => p);

            foreach (var vp in vmProps)
            {
                if (!entProps.TryGetValue(vp.Name, out var ep)) continue;
                if (!ep.CanWrite) continue;

                var vType = vp.PropertyType;
                var eType = ep.PropertyType;

                var val = vp.GetValue(vm);

                // Прямое присваивание, если совместимо по типу
                if (eType.IsAssignableFrom(vType))
                {
                    ep.SetValue(entity, val);
                    continue;
                }

                // Простейшие конверсии для частых кейсов (string->int, string->decimal, string->DateTime)
                try
                {
                    if (val is not null)
                    {
                        object? converted = null;

                        if (eType.IsEnum && val is string sEnum)
                            converted = Enum.Parse(eType, sEnum, ignoreCase: true);
                        else if (eType == typeof(Guid) && val is string sGuid && Guid.TryParse(sGuid, out var g))
                            converted = g;
                        else
                            converted = Convert.ChangeType(val, Nullable.GetUnderlyingType(eType) ?? eType);

                        if (converted is not null)
                            ep.SetValue(entity, converted);
                    }
                }
                catch
                {
                    // игнорируем несопоставимые поля
                }
            }

            // Проставим служебные поля
            var createdAt = entProps.Values.FirstOrDefault(p => p.Name.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase)
                                                             && (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?)));
            createdAt?.SetValue(entity, DateTime.UtcNow);

            // ---- Сохраняем
            await _feedbackService.AddFeedbackAsync(entity);

            // Можно показать "спасибо" или вернуть на главную
            return RedirectToAction("Index", "Home");
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel 
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}