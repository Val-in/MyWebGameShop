using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Services.Interfaces;

public interface IFeedbackService
{
    Task AddFeedbackAsync(FeedBackViewModel feedback);
    Task<List<FeedBackViewModel>> GetAllFeedbacksAsync();
}