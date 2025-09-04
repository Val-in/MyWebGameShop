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

    public async Task WriteLogAsync(Request entry)
    {
        _context.Requests.Add(entry);
        await _context.SaveChangesAsync();
    }
    
    public async Task LogTextFile(HttpContext context)
    {
        // Строка для публикации в лог
        string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
      
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
        await File.AppendAllTextAsync(logFilePath, logMessage);
        
        var users = _context.Users.ToList();
        Console.WriteLine("See all blog users:");
        foreach (var user in users)
            Console.WriteLine($"User name {user.UserName}, Balance {user.WalletBalance}");
    }
}