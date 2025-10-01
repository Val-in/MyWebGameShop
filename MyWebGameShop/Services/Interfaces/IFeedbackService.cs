using MyWebGameShop.Models; 

namespace MyWebGameShop.Services.Interfaces;

public interface IFeedbackService
{
    Task<Feedback> AddFeedbackAsync(Feedback feedback); 
    Task<List<Feedback>> GetAllFeedbacksAsync(); 
    Task<Feedback?> GetFeedbackByIdAsync(int id);
    Task<bool> DeleteFeedbackAsync(int id);
    Task<List<Feedback>> GetRecentFeedbacksAsync(int count = 10);
}