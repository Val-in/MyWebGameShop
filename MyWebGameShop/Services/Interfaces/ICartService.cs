using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ICartService
{
    Task<List<CartItem>> GetUserCartAsync(int userId);
    Task<CartItem?> AddToCartAsync(int userId, int gameId, int quantity = 1);
    Task<bool> UpdateCartItemAsync(int cartItemId, int quantity);
    Task<bool> RemoveFromCartAsync(int cartItemId);
    Task<bool> ClearUserCartAsync(int userId);
    Task<decimal> GetCartTotalAsync(int userId);
    Task<int> GetCartItemsCountAsync(int userId);
    Task<List<CartItem>> ConvertCartToOrderItemsAsync(int userId, Guid orderId);
}