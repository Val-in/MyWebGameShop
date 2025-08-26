using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("account")] //префикс для всех маршрутов в этом контроллере. Например, [HttpGet("register")] → /account/register
public class AccountController : Controller 
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signIn;
    
    public AccountController(IUserService userService,  SignInManager<User> signIn)
    {
        _userService = userService;
        _signIn = signIn;
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(); //возвращает Razor-View Register.cshtml
    }
    
    [HttpPost("register")]
    public async Task <IActionResult> Register (User newUser)
    {
        if (!ModelState.IsValid)
            return View(newUser);

        await _userService.AddUserAsync(newUser);

        return RedirectToAction("Login");
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