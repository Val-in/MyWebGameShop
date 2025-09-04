using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

public class UserController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger; //типированный, DI сможет корректно внедрить

    public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<UserViewModel> Authenticate(string login, string password) 
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException("Запрос некорректен");

        var user = await _userService.GetByLoginAsync(login);
        if (user == null)
            throw new AuthenticationException("Пользователь не найден");

        if (user.Password != password)
            throw new AuthenticationException("Введенный пароль не корректен");
        _logger.LogInformation("Пользователь {login} авторизовался", login);

        return _mapper.Map<UserViewModel>(user); 
        
    }
}