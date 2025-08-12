using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddToCartAsync(Guid userId, Guid productId, int quantity)
    {
        var cartItem = new CartItem
        {
            UserId = userId,
            GameId = productId,
            Quantity = quantity
        };
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(Guid cartItemId)
    {
        var item = await _context.CartItems.FindAsync(cartItemId);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<CartItem>> GetUserCartAsync(Guid userId)
    {
        return await _context.CartItems
            .Where(ci => ci.UserId == userId)
            .Include(ci => ci.Game)
            .ToListAsync();
    }
}