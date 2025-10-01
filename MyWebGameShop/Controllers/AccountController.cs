using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly IUserService _users;

    public AccountController(IUserService users) => _users = users;

    // ---------- Login ----------
    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var ok = await _users.VerifyPasswordAsync(vm.Email, vm.Password);
        if (!ok)
        {
            ModelState.AddModelError("", "Неверный email или пароль.");
            return View(vm);
        }

        var user = await _users.GetUserByEmailAsync(vm.Email);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Home");
    }

    // ---------- Register ----------
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var exists = await _users.GetUserByEmailAsync(vm.Email);
        if (exists != null)
        {
            ModelState.AddModelError(nameof(vm.Email), "Пользователь с таким email уже существует.");
            return View(vm);
        }

        // VM -> Entity
        var entity = new User
        {
            UserName = vm.UserName,
            LastName = vm.LastName ?? "",
            Login = vm.Login,
            Email = vm.Email,
            Password = vm.Password,   // TODO: захешировать при необходимости
            WalletBalance = 0,        // стартовое значение
            Joined = DateTime.UtcNow,
            Role = vm.Role           
        };

        await _users.AddUserAsync(entity);
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => Content("Access denied");
}
