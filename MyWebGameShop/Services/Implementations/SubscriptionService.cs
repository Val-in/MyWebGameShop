using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Enums;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subscriptions>> GetAllSubscriptionsAsync()
        => await _context.Subscriptions.ToListAsync();

    public async Task<Subscriptions?> GetSubscriptionByIdAsync(int id)
        => await _context.Subscriptions.FindAsync(id);

    public async Task<Subscriptions> CreateSubscriptionAsync(Subscriptions subscription)
    {
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
        return subscription;
    }

    public async Task<Subscriptions?> UpdateSubscriptionAsync(int id, Subscriptions subscription)
    {
        var existing = await _context.Subscriptions.FindAsync(id);
        if (existing == null) return null;

        existing.Name = subscription.Name;
        existing.Description = subscription.Description;
        existing.Price = subscription.Price;
        existing.SubscriptionType = subscription.SubscriptionType;
        existing.DurationMonths = subscription.DurationMonths;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteSubscriptionAsync(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription == null) return false;

        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Subscriptions>> GetSubscriptionsByTypeAsync(SubscriptionEnum type)
    {
        return await _context.Subscriptions
            .Where(s => s.SubscriptionType == type)
            .ToListAsync();
    }

    public async Task<List<SubscriptionUserInfo>> GetUserSubscriptionsAsync(int userId)
    {
        return await _context.SubscriptionUserInfos
            .Include(s => s.Subscription)
            .Include(s => s.User)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task<SubscriptionUserInfo> SubscribeUserAsync(int userId, int subscriptionId, string paymentMethod)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        if (subscription == null) throw new ArgumentException("Subscription not found");

        var userSubscription = new SubscriptionUserInfo
        {
            UserId = userId,
            SubscriptionId = subscriptionId,
            PaymentMethod = paymentMethod,
            SubscriptionStatus = "Active",
            SubscriptionStartDate = DateTime.Now,
            SubscriptionEndDate = DateTime.Now.AddMonths(subscription.DurationMonths),
            LastPaymentDate = DateTime.Now
        };

        _context.SubscriptionUserInfos.Add(userSubscription);
        await _context.SaveChangesAsync();
        return userSubscription;
    }

    public async Task<bool> CancelUserSubscriptionAsync(int userSubscriptionId)
    {
        var userSubscription = await _context.SubscriptionUserInfos.FindAsync(userSubscriptionId);
        if (userSubscription == null) return false;

        userSubscription.SubscriptionStatus = "Cancelled";
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Dictionary<SubscriptionEnum, int>> GetSubscriptionsCountByTypeAsync()
    {
        return await _context.Subscriptions
            .GroupBy(s => s.SubscriptionType)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    // ===== Алиасы для контроллера =====
    public Task<List<SubscriptionUserInfo>> GetSubscriptionsByUserAsync(int userId)
        => GetUserSubscriptionsAsync(userId);

    public Task<SubscriptionUserInfo> SubscribeAsync(int userId, int subscriptionId)
        => SubscribeUserAsync(userId, subscriptionId, "Card");

    public async Task<bool> UnsubscribeAsync(int userId, int subscriptionId)
    {
        var entity = await _context.SubscriptionUserInfos
            .FirstOrDefaultAsync(s => s.UserId == userId && s.SubscriptionId == subscriptionId);

        if (entity == null) return false;

        _context.SubscriptionUserInfos.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
