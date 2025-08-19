using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Game>> GetAllProductsAsync();
    Task<Game> GetProductByIdAsync(Guid productId);
    Task<List<Game>> SearchProductsAsync(string keyword);
}