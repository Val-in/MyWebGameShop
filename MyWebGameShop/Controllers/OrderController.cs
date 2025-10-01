using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("order")]
[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orders;

    public OrderController(IOrderService orders) => _orders = orders;

    public async Task<IActionResult> Index()
    {
        int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var list = await _orders.GetUserOrdersAsync(userId);
        return View(list);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var order = await _orders.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var order = await _orders.CreateOrderFromCartAsync(userId);
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }
}