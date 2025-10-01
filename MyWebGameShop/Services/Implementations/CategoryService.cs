using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Enums; // ✅ ДОБАВИТЬ
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Include(c => c.Games)
            .Include(c => c.Products)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Games)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateCategoryAsync(int id, Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null) return null;

        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        existingCategory.PlatformEnum = category.PlatformEnum;
        existingCategory.GenreEnum = category.GenreEnum;

        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    // ✅ ИСПРАВЛЕНО: используем Enum напрямую
    public async Task<List<Category>> GetCategoriesByPlatformAsync(PlatformEnum platform)
    {
        return await _context.Categories
            .Include(c => c.Games)
            .Include(c => c.Products)
            .Where(c => c.PlatformEnum == platform)
            .ToListAsync();
    }

    public async Task<List<Category>> GetCategoriesByGenreAsync(GenreEnum genre)
    {
        return await _context.Categories
            .Include(c => c.Games)
            .Include(c => c.Products)
            .Where(c => c.GenreEnum == genre)
            .ToListAsync();
    }

    // ✅ НОВЫЕ МЕТОДЫ С ENUMS
    public async Task<List<Category>> GetCategoriesByPlatformAndGenreAsync(PlatformEnum platform, GenreEnum genre)
    {
        return await _context.Categories
            .Include(c => c.Games)
            .Include(c => c.Products)
            .Where(c => c.PlatformEnum == platform && c.GenreEnum == genre)
            .ToListAsync();
    }

    public async Task<Dictionary<PlatformEnum, int>> GetCategoriesCountByPlatformAsync()
    {
        return await _context.Categories
            .GroupBy(c => c.PlatformEnum)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task<Dictionary<GenreEnum, int>> GetCategoriesCountByGenreAsync()
    {
        return await _context.Categories
            .GroupBy(c => c.GenreEnum)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }
}