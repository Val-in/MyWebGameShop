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

    public async Task<List<CartItem>> GetUserCartAsync(int userId)
    {
        return await _context.CartItems
            .Include(c => c.Game)
            .ThenInclude(g => g.Category)
            .Where(c => c.UserId == userId && c.OrderId == null)
            .ToListAsync();
    }

    public async Task<CartItem?> AddToCartAsync(int userId, int gameId, int quantity = 1)
    {
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.GameId == gameId && c.OrderId == null);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            await _context.SaveChangesAsync();
            return existingItem;
        }

        var newItem = new CartItem
        {
            UserId = userId,
            GameId = gameId,
            Quantity = quantity
        };

        _context.CartItems.Add(newItem);
        await _context.SaveChangesAsync();
        return newItem;
    }

    public async Task<bool> UpdateCartItemAsync(int cartItemId, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null) return false;

        if (quantity <= 0)
        {
            _context.CartItems.Remove(cartItem);
        }
        else
        {
            cartItem.Quantity = quantity;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null) return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearUserCartAsync(int userId)
    {
        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId && c.OrderId == null)
            .ToListAsync();

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetCartTotalAsync(int userId)
    {
        return await _context.CartItems
            .Include(c => c.Game)
            .Where(c => c.UserId == userId && c.OrderId == null)
            .SumAsync(c => c.Game.Price * c.Quantity);
    }

    public async Task<int> GetCartItemsCountAsync(int userId)
    {
        return await _context.CartItems
            .Where(c => c.UserId == userId && c.OrderId == null)
            .SumAsync(c => c.Quantity);
    }

    public async Task<List<CartItem>> ConvertCartToOrderItemsAsync(int userId, Guid orderId)
    {
        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId && c.OrderId == null)
            .ToListAsync();

        foreach (var item in cartItems)
        {
            item.OrderId = orderId;
        }

        await _context.SaveChangesAsync();
        return cartItems;
    }
}