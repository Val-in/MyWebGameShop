using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<List<Product>> SearchProductsAsync(string searchTerm);
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> UpdateStockAsync(int productId, int quantity);
    Task<List<Product>> GetFeaturedProductsAsync();
    Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}