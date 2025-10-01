using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class GameService : IGameService
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        return await _context.Games
            .Include(g => g.Category)
            .ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Category)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Game>> GetGamesByCategoryAsync(int categoryId)
    {
        return await _context.Games
            .Include(g => g.Category)
            .Where(g => g.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Game>> SearchGamesAsync(string searchTerm)
    {
        return await _context.Games
            .Include(g => g.Category)
            .Where(g => g.Title.Contains(searchTerm) || g.Description.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<List<Game>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Games
            .Include(g => g.Category)
            .Where(g => g.Price >= minPrice && g.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<Game> CreateGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> UpdateGameAsync(int id, Game game)
    {
        var existingGame = await _context.Games.FindAsync(id);
        if (existingGame == null) return null;

        existingGame.Title = game.Title;
        existingGame.Description = game.Description;
        existingGame.Price = game.Price;
        existingGame.ImageUrl = game.ImageUrl;
        existingGame.CategoryId = game.CategoryId;

        await _context.SaveChangesAsync();
        return existingGame;
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return false;

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Game>> GetFeaturedGamesAsync()
    {
        return await _context.Games
            .Include(g => g.Category)
            .Take(6)
            .ToListAsync();
    }
}
