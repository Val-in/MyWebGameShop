using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("products")]
public class ProductsController : Controller
{
    private readonly IProductService _products;
    private readonly IMapper _mapper;

    public ProductsController(IProductService products, IMapper mapper)
    {
        _products = products;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _products.GetAllProductsAsync();
        var vm = _mapper.Map<List<GameDetailsViewModel>>(items);
        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _products.GetProductByIdAsync(id);
        if (item == null) return NotFound();
        var vm = _mapper.Map<GameDetailsViewModel>(item);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string q)
    {
        var items = string.IsNullOrWhiteSpace(q)
            ? await _products.GetAllProductsAsync()
            : await _products.SearchProductsAsync(q);
        var vm = _mapper.Map<List<GameDetailsViewModel>>(items);
        ViewBag.Query = q;
        return View("Index", vm);
    }
}