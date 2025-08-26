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
        try
        {
            var logEntry = new Request()
            {
                Id = Guid.NewGuid(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                Date = DateTime.UtcNow,
                Url = context.Request.Path + context.Request.QueryString
            };

            await _context.Requests.AddAsync(logEntry);
            await _context.SaveChangesAsync();

            _logger.LogInformation("New request: {method} {url}", context.Request.Method, context.Request.Path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging request");
        }

        await _next(context); //_next — это делегат RequestDelegate, который указывает, какой middleware будет выполняться после текущего.
                             //_next гарантирует, что все остальные middleware и MVC-контроллеры тоже выполнятся.
    }
 
    //Вынести в сервис
    // private async Task LogFile(HttpContext context)
    // {
    //     // Строка для публикации в лог
    //     string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
    //   
    //     string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
    //     Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
    //     await File.AppendAllTextAsync(logFilePath, logMessage);
    //     
    //     var users = _context.Users.ToList();
    //     Console.WriteLine("See all blog users:");
    //     foreach (var user in users)
    //         Console.WriteLine($"User name {user.UserName}, Balance {user.WalletBalance}");
    // }
}