using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("game")]
public class GameController : Controller
{
    private readonly IGameService _games;
    private readonly ICategoryService _categories;
    private readonly IMapper _mapper;

    public GameController(IGameService games, ICategoryService categories, IMapper mapper)
    {
        _games = games;
        _categories = categories;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _games.GetAllGamesAsync();
        var vm = _mapper.Map<List<GameDetailsViewModel>>(items);
        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        var g = await _games.GetGameByIdAsync(id);
        if (g == null) return NotFound();
        var vm = _mapper.Map<GameDetailsViewModel>(g);
        return View(vm);
    }

    [HttpGet("game/category/{categoryId:int}")]
    public async Task<IActionResult> ByCategory(int categoryId)
    {
        var cat = await _categories.GetCategoryByIdAsync(categoryId);
        if (cat == null) return NotFound();

        var items = await _games.GetGamesByCategoryAsync(categoryId);
        var vm = _mapper.Map<List<GameDetailsViewModel>>(items);
        ViewBag.Category = cat.Name;
        return View("Index", vm);
    }

    [HttpGet("game/search")]
    public async Task<IActionResult> Search(string q)
    {
        var items = string.IsNullOrWhiteSpace(q)
            ? await _games.GetAllGamesAsync()
            : await _games.SearchGamesAsync(q);
        var vm = _mapper.Map<List<GameDetailsViewModel>>(items);
        ViewBag.Query = q;
        return View("Index", vm);
    }
}