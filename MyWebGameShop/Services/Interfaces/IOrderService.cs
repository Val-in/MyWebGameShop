using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int userId); 
    Task<List<Order>> GetOrderByUserAsync(int userId);
}