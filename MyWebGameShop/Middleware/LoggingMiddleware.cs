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
        var logEntry = new Request()
        {
            Id = Guid.NewGuid(),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            Date = DateTime.UtcNow,
            Url = context.Request.Path + context.Request.QueryString,
            IsLog = true
        };
        try
        {
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
    
}