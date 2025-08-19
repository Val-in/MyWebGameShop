using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;

namespace MyWebGameShop.Controllers;

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
}