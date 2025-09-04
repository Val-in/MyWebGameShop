using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("logs")]
public class LogController : Controller // //LogController → просмотр/ручное добавление логов
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    [HttpPost]
    public async Task<IActionResult> WriteLogAsync(Request entry)
    {
        await _logService.WriteLogAsync(entry);
        return Ok();
    }
    
    //Вывести все логи
    [HttpGet("logs")] //запрос подконтроллера => получится logs/logs
    public async Task<IActionResult> Index([FromServices] AppDbContext db) //Пользователь → контроллер → создание Request → вызов LogService →
                                             //_context.Requests.Add() → SaveChangesAsync() → EF → SQL INSERT → Requests в БД
    {
        var data = await db.Requests
            .OrderByDescending(r => r.Date)
            .ToListAsync();
        return View(data);
    }
}