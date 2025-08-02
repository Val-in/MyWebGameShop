using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Controllers;
using MyWebGameShop.Models;

namespace MyWebGameShop.ViewModels;

public class RegisterViewModel //запуталась, что куда передаем...
{
    [HttpPost]
    public async Task Register(User user)
    {
        user.Joined = DateTime.Now;
        user.Id = 3;
 
        // Добавление пользователя
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.Users.AddAsync(user);
  
        // Сохранение изменений
        await _context.SaveChangesAsync();
        await Console.WriteLine($"Registration successful, {user.UserName}");
    }
}