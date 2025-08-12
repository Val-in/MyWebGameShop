using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> GetAllProductsAsync()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<Game> GetProductByIdAsync(Guid productId)
    {
        return await _context.Games.FindAsync(productId);
    }

    public async Task<List<Game>> SearchProductsAsync(string keyword)
    {
        return await _context.Games
            .Where(g => g.Title.Contains(keyword))
            .ToListAsync();
    }
}