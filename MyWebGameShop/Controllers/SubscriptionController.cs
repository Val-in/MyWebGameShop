using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("subscription")]
[Authorize]
public class SubscriptionController : Controller
{
    private readonly ISubscriptionService _subs;

    public SubscriptionController(ISubscriptionService subs) => _subs = subs;

    public async Task<IActionResult> Index()
    {
        var all = await _subs.GetAllSubscriptionsAsync();
        return View(all);
    }

    public async Task<IActionResult> My()
    {
        int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var list = await _subs.GetSubscriptionsByUserAsync(userId);
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(int id)
    {
        int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        await _subs.SubscribeAsync(userId, id);
        return RedirectToAction(nameof(My));
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(int id)
    {
        int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        await _subs.UnsubscribeAsync(userId, id);
        return RedirectToAction(nameof(My));
    }
}