using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid userId);
    Task<List<Order>> GetUserOrdersAsync(Guid userId);
}