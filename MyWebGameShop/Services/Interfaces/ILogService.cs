using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ILogService
{
    Task LogRequestAsync(string url, string userAgent, string entry = "", bool isLog = true);
    Task<List<Request>> GetAllRequestsAsync();
    Task<List<Request>> GetRecentRequestsAsync(int count = 100);
    Task<List<Request>> GetRequestsByDateAsync(DateTime date);
    Task<int> GetRequestsCountAsync();
    Task ClearOldRequestsAsync(int daysOld = 30);
    
    Task WriteLogAsync(Request entry);
}