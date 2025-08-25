using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;

namespace MyWebGameShop.Controllers;

[Route("logs")] //везде добавить слеши
public class LogController : Controller
{
    private readonly AppDbContext _dbContext;

    public LogController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs()
    {
        var logs = await _dbContext.Logs
            .OrderByDescending(l => l.Timestamp)
            .ToListAsync();
        return Ok(logs);
    }
    
    [HttpGet("/logs")] //запрос подконтроллера => получится logs/logs
    public async Task<IActionResult> Index()
    {
        // показываем то, что требовалось в задании 32.11.1 — из таблицы Requests
        var data = await _dbContext.Requests
            .OrderByDescending(r => r.Date)
            .ToListAsync();
        return View(data);
    }
}