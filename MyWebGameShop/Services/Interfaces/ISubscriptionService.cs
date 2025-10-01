using MyWebGameShop.Models;
using MyWebGameShop.Enums;

namespace MyWebGameShop.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<Subscriptions>> GetAllSubscriptionsAsync();
    Task<Subscriptions?> GetSubscriptionByIdAsync(int id);
    Task<Subscriptions> CreateSubscriptionAsync(Subscriptions subscription);
    Task<Subscriptions?> UpdateSubscriptionAsync(int id, Subscriptions subscription);
    Task<bool> DeleteSubscriptionAsync(int id);
    Task<List<Subscriptions>> GetSubscriptionsByTypeAsync(SubscriptionEnum type);
    Task<List<SubscriptionUserInfo>> GetUserSubscriptionsAsync(int userId);
    Task<SubscriptionUserInfo> SubscribeUserAsync(int userId, int subscriptionId, string paymentMethod);
    Task<bool> CancelUserSubscriptionAsync(int userSubscriptionId);
    Task<Dictionary<SubscriptionEnum, int>> GetSubscriptionsCountByTypeAsync();

    // Алиасы для контроллера
    Task<List<SubscriptionUserInfo>> GetSubscriptionsByUserAsync(int userId);
    Task<SubscriptionUserInfo> SubscribeAsync(int userId, int subscriptionId);
    Task<bool> UnsubscribeAsync(int userId, int subscriptionId);
}