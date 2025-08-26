using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("products")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        var viewModels = _mapper.Map<List<GameDetailsViewModel>>(products);
        return View(viewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var game = await _productService.GetProductByIdAsync(id);
        if (game == null) return NotFound();

        var viewModel = _mapper.Map<GameDetailsViewModel>(game);
        return View(viewModel);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string keyword)
    {
        var results = await _productService.SearchProductsAsync(keyword);
        var viewModels = _mapper.Map<List<GameDetailsViewModel>>(results);
        return View("Index", viewModels);
    }
}