using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;
using MyWebGameShop.Models;

namespace MyWebGameShop.Controllers;

[Route("recommendation")]
public class RecommendationController : Controller
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    public async Task<IActionResult> Index()
    {
        var recommendations = await _recommendationService.GetAllRecommendationsAsync();
        var viewModels = recommendations.Select(r => new RecommendationViewModel
        {
            
            Id = r.Id,
            Description = r.Description,
            GameTitle = r.GameTitle,
            
            GameVersion = r.GameVersion,
            GameRate = r.GameRate,
            RecommendationComment = r.RecommendationComment,
            UserName = r.User?.UserName ?? "Anonymous"
        }).ToList();

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new RecommendationViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(RecommendationViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // В реальном приложении получать UserId из сессии/аутентификации
        var recommendation = new Recommendation
        {
            Description = model.Description,
            GameTitle = model.GameTitle,
            GameVersion = model.GameVersion,
            GameRate = model.GameRate,
            RecommendationComment = model.RecommendationComment,
            UserId = 1 // Заглушка
        };

        await _recommendationService.CreateRecommendationAsync(recommendation);
        return RedirectToAction(nameof(Index));
    }
}
