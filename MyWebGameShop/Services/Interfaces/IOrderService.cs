using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid userId); //int
    Task<List<Order>> GetOrderByUserAsync(Guid userId);
}