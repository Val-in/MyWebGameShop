using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("subscriptions")]
public class SubscriptionController : Controller
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IMapper _mapper;

    public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
    {
        _subscriptionService = subscriptionService;
        _mapper = mapper;
    }

    // Показать все подписки пользователя
    [HttpGet("{userId}")]
    public async Task<IActionResult> Index(int userId)
    {
        var subs = await _subscriptionService.GetSubscriptionsByUserAsync(userId);

        // Маппим через AutoMapper
        var vm = _mapper.Map<List<SubscriptionsViewModel>>(subs);

        return View(vm);
    }

    // Подписаться
    [HttpPost("{userId}/subscribe/{subscriptionId}")]
    public async Task<IActionResult> Subscribe(int userId, int subscriptionId)
    {
        await _subscriptionService.SubscribeAsync(userId, subscriptionId);
        return RedirectToAction("Index", new { userId });
    }

    // Отписаться
    [HttpPost("{userId}/unsubscribe/{subscriptionId}")]
    public async Task<IActionResult> Unsubscribe(int userId, int subscriptionId)
    {
        await _subscriptionService.UnsubscribeAsync(userId, subscriptionId);
        return RedirectToAction("Index", new { userId });
    }
}