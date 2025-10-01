using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly ICartService _cartService;

    public OrderService(AppDbContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.CartItems)
            .ThenInclude(c => c.Game)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.CartItems)
            .ThenInclude(c => c.Game)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<Order> CreateOrderFromCartAsync(int userId)
    {
        var cartTotal = await _cartService.GetCartTotalAsync(userId);
        if (cartTotal == 0) throw new InvalidOperationException("Корзина пуста");

        var order = new Order
        {
            UserId = userId,
            TotalAmount = cartTotal,
            OrderDate = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _cartService.ConvertCartToOrderItemsAsync(userId, order.Id);

        return order;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> CancelOrderAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        // Возвращаем товары в корзину
        var orderItems = await _context.CartItems
            .Where(c => c.OrderId == orderId)
            .ToListAsync();

        foreach (var item in orderItems)
        {
            item.OrderId = null;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetOrderTotalAsync(Guid orderId)
    {
        return await _context.CartItems
            .Include(c => c.Game)
            .Where(c => c.OrderId == orderId)
            .SumAsync(c => c.Game.Price * c.Quantity);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.CartItems)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<List<Order>> GetRecentOrdersAsync(int count = 10)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.CartItems)
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .ToListAsync();
    }
    
    public Task<List<Order>> GetOrderByUserAsync(int userId) => GetUserOrdersAsync(userId);

    public Task<Order> CreateOrderAsync(int userId) => CreateOrderFromCartAsync(userId);
}