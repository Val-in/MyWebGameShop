using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface ILogService
{
    Task WriteLogAsync(Request entry);
}