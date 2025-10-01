using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

//[Route("home")]
public class HomeController : Controller
{
    private readonly IProductService _products;
    private readonly IGameService _games;
    private readonly IRecommendationService _reco;
    private readonly IMapper _mapper;

    public HomeController(IProductService products, IGameService games, IRecommendationService reco, IMapper mapper)
    {
        _products = products;
        _games = games;
        _reco = reco;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _games.GetAllGamesAsync();
        var products = await _products.GetAllProductsAsync();

        var vm = new HomePageViewModel
        {
            Games = _mapper.Map<List<GameDetailsViewModel>>(games.Take(8).ToList()),
            Products = _mapper.Map<List<GameDetailsViewModel>>(products.Take(8).ToList())
        };

        return View(vm);
    }

    public IActionResult Error() => View();
}