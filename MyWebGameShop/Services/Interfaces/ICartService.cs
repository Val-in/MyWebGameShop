using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ICartService //дописать сервисы
{
    Task AddToCartAsync(Guid userId, Guid productId, int quantity); //int
    Task RemoveFromCartAsync(Guid cartItemId);
    Task<List<CartItem>> GetUserCartAsync(Guid userId);
    Task ClearCartAsync(Guid cartItemId);
}