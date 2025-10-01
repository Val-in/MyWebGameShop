using MyWebGameShop.Models;
using MyWebGameShop.Enums;

namespace MyWebGameShop.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category?> UpdateCategoryAsync(int id, Category category);
    Task<bool> DeleteCategoryAsync(int id);

    Task<List<Category>> GetCategoriesByPlatformAsync(PlatformEnum platform);
    Task<List<Category>> GetCategoriesByGenreAsync(GenreEnum genre);

    Task<List<Category>> GetCategoriesByPlatformAndGenreAsync(PlatformEnum platform, GenreEnum genre);
    Task<Dictionary<PlatformEnum, int>> GetCategoriesCountByPlatformAsync();
    Task<Dictionary<GenreEnum, int>> GetCategoriesCountByGenreAsync();
}