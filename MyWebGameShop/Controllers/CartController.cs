using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("cart")]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    public IActionResult Index(int userId)
    {
        var cartItems = _cartService.GetUserCartAsync(userId);
        return View(cartItems); // отдать список в представление
    }
    
    [HttpPost]
    public IActionResult Add(int userId, int productId, int quantity = 1)
    {
        _cartService.AddToCartAsync(userId, productId, quantity);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cartService.RemoveFromCartAsync(productId);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Clear(int userId)
    {
        _cartService.ClearCartAsync(userId);
        return RedirectToAction("Index");
    }
    
}