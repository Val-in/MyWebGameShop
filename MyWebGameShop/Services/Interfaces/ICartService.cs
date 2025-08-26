using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ICartService 
{
    Task AddToCartAsync(int userId, int productId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task<List<CartItem>> GetUserCartAsync(int userId);
    Task ClearCartAsync(int cartItemId);
}