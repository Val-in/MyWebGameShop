using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class LogService : ILogService
{
    private readonly AppDbContext _context;

    public LogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LogRequestAsync(string url, string userAgent, string entry = "", bool isLog = true)
    {
        var request = new Request
        {
            Url = url,
            UserAgent = userAgent,
            Entry = entry,
            IsLog = isLog,
            Date = DateTime.Now
        };

        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Request>> GetAllRequestsAsync()
    {
        return await _context.Requests
            .OrderByDescending(r => r.Date)
            .ToListAsync();
    }

    public async Task<List<Request>> GetRecentRequestsAsync(int count = 100)
    {
        return await _context.Requests
            .OrderByDescending(r => r.Date)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Request>> GetRequestsByDateAsync(DateTime date)
    {
        return await _context.Requests
            .Where(r => r.Date.Date == date.Date)
            .OrderByDescending(r => r.Date)
            .ToListAsync();
    }

    public async Task<int> GetRequestsCountAsync()
    {
        return await _context.Requests.CountAsync();
    }

    public async Task ClearOldRequestsAsync(int daysOld = 30)
    {
        var cutoffDate = DateTime.Now.AddDays(-daysOld);
        var oldRequests = await _context.Requests
            .Where(r => r.Date < cutoffDate)
            .ToListAsync();

        _context.Requests.RemoveRange(oldRequests);
        await _context.SaveChangesAsync();
    }

    public async Task WriteLogAsync(Request entry)
    {
        entry.Date = entry.Date == default ? DateTime.Now : entry.Date;
        _context.Requests.Add(entry);
        await _context.SaveChangesAsync();
    }
}
