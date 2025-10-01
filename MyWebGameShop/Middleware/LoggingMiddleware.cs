using MyWebGameShop.Data;
using MyWebGameShop.Models;

namespace MyWebGameShop.Middleware;
/// <summary>
/// InvalidOperationException: Cannot resolve scoped service 'MyWebGameShop.Data.AppDbContext' from root provider.
/// </summary>
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory; //создаём scope вручную, чтобы получить DbContext безопасно в singleton middleware

  
    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory; 
    }
  
    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = _scopeFactory.CreateScope(); //scope для DbContext на каждый запрос
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var logEntry = new Request()
        {
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            Date = DateTime.UtcNow,
            Url = context.Request.Path + context.Request.QueryString,
            IsLog = true,
            Entry = string.Empty
        };

        try
        {
            await db.Requests.AddAsync(logEntry);
            await db.SaveChangesAsync();

            _logger.LogInformation("New request: {method} {url}", context.Request.Method, context.Request.Path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging request");
        }

        await _next(context);
    }
    
}