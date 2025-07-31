using MyWebGameShop.Models;
using MyWebGameShop.Services.Implementations;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly UserService _userService;
  
    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, UserService userService)
    {
        _next = next;
        _logger = logger;
        _userService = userService;
    }
  
    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        string userAgent = context.Request.Headers["User-Agent"].ToString();
        
        var newUser = new User()
        {
            Id = 1,
            UserName = "Vova",
            UserAgent = userAgent,
            Login = "KK",
            Password = "Pass",
            Email = "aa@aa.com",
            WalletBalance = 10
        };
        
        _userService.AddUser(newUser);
        LogConsole(context);
        await LogFile(context);
  
        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
    
    private async void LogConsole(HttpContext context)
    {
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