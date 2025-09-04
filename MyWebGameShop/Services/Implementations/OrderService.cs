using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(int userId)
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

    public async Task<List<Order>> GetOrderByUserAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Game)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrderByIdAsync(Guid id)
    {
        return _context.Orders
            .Where(o => o.Id == id)
            .Include(o => o.CartItems)
            .ToList();
    }
}
