using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IRecommendationService
{
    Task<List<Recommendation>> GetAllRecommendationsAsync();
    Task<List<Recommendation>> GetUserRecommendationsAsync(int userId);
    Task<Recommendation?> GetRecommendationByIdAsync(int id);
    Task<Recommendation> CreateRecommendationAsync(Recommendation recommendation);
    Task<Recommendation?> UpdateRecommendationAsync(int id, Recommendation recommendation);
    Task<bool> DeleteRecommendationAsync(int id);
    Task<List<Recommendation>> GetRecommendationsByGameTitleAsync(string gameTitle);
    Task<List<Recommendation>> GetTopRatedRecommendationsAsync(int count = 10);
}
