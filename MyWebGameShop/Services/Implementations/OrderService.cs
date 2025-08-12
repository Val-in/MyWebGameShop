using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class OrderService : IOrderService //что тут не так, почему ошибка?
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Guid userId)
    {
        var cartItems = await _context.CartItems
            .Where(ci => ci.UserId == userId)
            .ToListAsync();

        var order = new Order
        {
            UserId = userId,
            CartItems = cartItems
        };

        _context.Orders.Add(order);
        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Game)
            .ToListAsync();
    }
}
