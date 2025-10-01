using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IGameService
{
    Task<List<Game>> GetAllGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<List<Game>> GetGamesByCategoryAsync(int categoryId);
    Task<List<Game>> SearchGamesAsync(string searchTerm);
    Task<List<Game>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<Game> CreateGameAsync(Game game);
    Task<Game?> UpdateGameAsync(int id, Game game);
    Task<bool> DeleteGameAsync(int id);
    Task<List<Game>> GetFeaturedGamesAsync();
}
