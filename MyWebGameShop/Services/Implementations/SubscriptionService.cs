using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionUserInfo>> GetSubscriptionsByUserAsync(int userId)
    {
        return await _context.SubscriptionUserInfos
            .Include(u => u.Subscription) // подгружаем саму подписку
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }

    public async Task SubscribeAsync(int userId, int subscriptionId)
    {
        // проверим, есть ли уже такая подписка
        var existing = await _context.SubscriptionUserInfos
            .FirstOrDefaultAsync(u => u.UserId == userId && u.SubscriptionId == subscriptionId);

        if (existing == null)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId); //это нам нужно, чтобы взять enum типа подписки
            var newSub = new SubscriptionUserInfo
            {
                UserId = userId,
                SubscriptionId = subscriptionId,
                Subscription = subscription,
                SubscriptionStatus = "Active",
                SubscriptionStartDate = DateTime.UtcNow,
                SubscriptionEndDate = DateTime.UtcNow.AddMonths(1), // например, на месяц
                LastPaymentDate = DateTime.UtcNow,
                PaymentHistory = "",
                PaymentMethod = "Unknown",
            };

            _context.SubscriptionUserInfos.Add(newSub);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UnsubscribeAsync(int userId, int subscriptionId)
    {
        var existing = await _context.SubscriptionUserInfos
            .FirstOrDefaultAsync(u => u.UserId == userId && u.SubscriptionId == subscriptionId);

        if (existing != null)
        {
            _context.SubscriptionUserInfos.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}