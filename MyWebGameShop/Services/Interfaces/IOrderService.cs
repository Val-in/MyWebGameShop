using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetUserOrdersAsync(int userId);
    Task<Order?> GetOrderByIdAsync(Guid orderId);
    Task<Order> CreateOrderFromCartAsync(int userId);
    Task<Order> CreateOrderAsync(Order order);
    Task<bool> CancelOrderAsync(Guid orderId);
    Task<decimal> GetOrderTotalAsync(Guid orderId);
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetRecentOrdersAsync(int count = 10);
    
    Task<List<Order>> GetOrderByUserAsync(int userId); // alias GetUserOrdersAsync
    Task<Order> CreateOrderAsync(int userId);          // alias CreateOrderFromCartAsync
}