using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Implementations;

namespace MyWebGameShop.Controllers;

[Route("account")]
public class AccountController : Controller 
{
    private readonly UserService _userService;
  
    // Метод-конструктор для инициализации
    public AccountController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost("register")]
    public async Task <IActionResult> Register (User newUser)
    {
        await _userService.AddUser(newUser);
        return View(newUser);
    }
    
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = null) =>
        View(new LoginViewModel());

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var res = await _signIn.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
        if (!res.Succeeded)
        {
            ModelState.AddModelError("", "Неверные учётные данные");
            return View(vm);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}