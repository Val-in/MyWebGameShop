using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<Subscriptions>> GetSubscriptionsByUserAsync(User id);
    Task SubscribeAsync(User id, int subscriptionId);
    Task UnsubscribeAsync(User id, int subscriptionId);
}