using MyWebGameShop.Data;
using MyWebGameShop.Models;

namespace MyWebGameShop.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly AppDbContext _context;
  
    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, AppDbContext context)
    {
        _next = next;
        _logger = logger;
        _context = context;
    }
  
    public async Task InvokeAsync(HttpContext context)
    {
        string userAgent = context.Request.Headers["User-Agent"].ToString();

        var logEntry = new LogEntry
        {
            UserAgent = userAgent,
            Timestamp = DateTime.UtcNow
        };

        await _context.Logs.AddAsync(logEntry);
        await _context.SaveChangesAsync();

        _logger.LogInformation("New request: {method} {url}", context.Request.Method, context.Request.Path);

        await _next(context);
    }
 
    private async Task LogFile(HttpContext context)
    {
        // Строка для публикации в лог
        string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
      
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
        await File.AppendAllTextAsync(logFilePath, logMessage);
    }
}