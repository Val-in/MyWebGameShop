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

    public async Task WriteLogAsync(LogEntry entry)
    {
        _context.Logs.Add(entry);
        await _context.SaveChangesAsync();
    }
}