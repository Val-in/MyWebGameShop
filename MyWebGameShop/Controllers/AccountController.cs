using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Implementations;

namespace MyWebGameShop.Controllers;

public class AccountController : Controller 
{
    private readonly UserService _userService;
  
    // Метод-конструктор для инициализации
    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task <IActionResult> Register (User newUser)
    {
        await _userService.AddUser(newUser);
        return View(newUser);
    }
    
    /*[HttpPost]
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
    }  как работает эта логика в примере*/
}