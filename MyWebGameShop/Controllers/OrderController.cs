using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

public class OrderController : Controller //здесь мы используем ручной mapping
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(int userId)
    {
        var orders = await _orderService.GetOrderByUserAsync(userId);

        // Маппинг из модели Order → OrderViewModel
        var orderVms = orders.Select(o => new OrderViewModel
        {
            Id = o.Id,
            CreatedAt = o.OrderDate,
            TotalAmount = o.CartItems.Sum(ci => ci.Game.Price * ci.Quantity),
            Items = o.CartItems.Select(ci => new CartItemViewModel
            {
                GameId = ci.GameId,
                Title = ci.Game.Title,
                Price = ci.Game.Price,
                Quantity = ci.Quantity
            }).ToList()
        }).ToList();

        return View(orderVms); // либо return Json(orderVms); если нужен API
    }

    [HttpPost("create/{userId}")]     // POST /orders/create/5
    public async Task<IActionResult> CreateOrder(int userId)
    {
        var order = await _orderService.CreateOrderAsync(userId);

        var orderVm = new OrderViewModel
        {
            Id = order.Id,
            CreatedAt = order.OrderDate,
            TotalAmount = order.CartItems.Sum(ci => ci.Game.Price * ci.Quantity),
            Items = order.CartItems.Select(ci => new CartItemViewModel
            {
                GameId = ci.GameId,
                Title = ci.Game.Title,
                Price = ci.Game.Price,
                Quantity = ci.Quantity
            }).ToList()
        };

        return View("OrderDetails", orderVm); // Страница с деталями конкретного заказа
    }
}