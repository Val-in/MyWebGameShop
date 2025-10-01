using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Controllers;

[Route("log")]
public class LogController : Controller
{
    private readonly ILogService _log;

    public LogController(ILogService log) => _log = log;

    public async Task<IActionResult> Index()
    {
        var last = await _log.GetRecentRequestsAsync(100);
        return View(last);
    }

    [HttpPost]
    public async Task<IActionResult> Write(string entry)
    {
        if (string.IsNullOrWhiteSpace(entry))
            return RedirectToAction(nameof(Index));

        await _log.WriteLogAsync(new Request
        {
            Url = "[manual]",
            UserAgent = Request.Headers.UserAgent.ToString(),
            Entry = entry,
            IsLog = true,
            Date = DateTime.Now
        });

        return RedirectToAction(nameof(Index));
    }
}