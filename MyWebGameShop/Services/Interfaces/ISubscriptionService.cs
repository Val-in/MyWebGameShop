using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<SubscriptionUserInfo>> GetSubscriptionsByUserAsync(int userId);
    Task SubscribeAsync(int userId, int subscriptionId); //Что передаём: только идентификаторы (числа) пользователя и подписки.
                                                         //Зачем: сервис сам ищет объекты в базе по этим id через DbContext.
    
    Task UnsubscribeAsync(int userId, int subscriptionId);
}