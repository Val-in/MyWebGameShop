using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Models;

namespace MyWebGameShop.Controllers;

public class AccountController : Controller //почему нельзя от него отнаследоваться?
{
    public AccountController() //нужен конструктор?
    {
        
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
}