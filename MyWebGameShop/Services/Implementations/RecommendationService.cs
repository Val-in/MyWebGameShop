using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class RecommendationService : IRecommendationService
{
    private readonly AppDbContext _context;

    public RecommendationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recommendation>> GetAllRecommendationsAsync()
    {
        return await _context.Recommendations
            .Include(r => r.User)
            .OrderByDescending(r => r.GameRate)
            .ToListAsync();
    }

    public async Task<List<Recommendation>> GetUserRecommendationsAsync(int userId)
    {
        return await _context.Recommendations
            .Include(r => r.User)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.GameRate)
            .ToListAsync();
    }

    public async Task<Recommendation?> GetRecommendationByIdAsync(int id)
    {
        return await _context.Recommendations
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recommendation> CreateRecommendationAsync(Recommendation recommendation)
    {
        _context.Recommendations.Add(recommendation);
        await _context.SaveChangesAsync();
        return recommendation;
    }

    public async Task<Recommendation?> UpdateRecommendationAsync(int id, Recommendation recommendation)
    {
        var existingRecommendation = await _context.Recommendations.FindAsync(id);
        if (existingRecommendation == null) return null;

        existingRecommendation.Description = recommendation.Description;
        existingRecommendation.GameTitle = recommendation.GameTitle;
        existingRecommendation.GameVersion = recommendation.GameVersion;
        existingRecommendation.GameRate = recommendation.GameRate;
        existingRecommendation.RecommendationComment = recommendation.RecommendationComment;

        await _context.SaveChangesAsync();
        return existingRecommendation;
    }

    public async Task<bool> DeleteRecommendationAsync(int id)
    {
        var recommendation = await _context.Recommendations.FindAsync(id);
        if (recommendation == null) return false;

        _context.Recommendations.Remove(recommendation);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Recommendation>> GetRecommendationsByGameTitleAsync(string gameTitle)
    {
        return await _context.Recommendations
            .Include(r => r.User)
            .Where(r => r.GameTitle.Contains(gameTitle))
            .OrderByDescending(r => r.GameRate)
            .ToListAsync();
    }

    public async Task<List<Recommendation>> GetTopRatedRecommendationsAsync(int count = 10)
    {
        return await _context.Recommendations
            .Include(r => r.User)
            .OrderByDescending(r => r.GameRate)
            .Take(count)
            .ToListAsync();
    }
}
