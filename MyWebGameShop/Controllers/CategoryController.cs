using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Enums;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> ByPlatform(PlatformEnum platform)
    {
        var categories = await _categoryService.GetCategoriesByPlatformAsync(platform);
        ViewBag.Platform = platform;
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> ByGenre(GenreEnum genre)
    {
        var categories = await _categoryService.GetCategoriesByGenreAsync(genre);
        ViewBag.Genre = genre;
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(PlatformEnum? platform, GenreEnum? genre)
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        if (platform.HasValue)
            categories = categories.Where(c => c.PlatformEnum == platform.Value).ToList();
            
        if (genre.HasValue)
            categories = categories.Where(c => c.GenreEnum == genre.Value).ToList();

        ViewBag.Platforms = Enum.GetValues<PlatformEnum>();
        ViewBag.Genres = Enum.GetValues<GenreEnum>();
        
        return View(categories);
    }
}
