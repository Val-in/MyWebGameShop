using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.Controllers;

public class UserController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("authenticate")]
    public UserViewModel Authenticate(string login, string password) //нужен ли nuget cookies?
    {
        if (String.IsNullOrEmpty(login) ||
            String.IsNullOrEmpty(password))
            throw new ArgumentNullException("Запрос не корректен");

        User user = _userService.GetByLogin(login);
        if (user is null)
            throw new AuthenticationException("Пользователь на найден");

        if (user.Password != password)
            throw new AuthenticationException("Введенный пароль не корректен");

        return _mapper.Map < UserViewModel > (user); //как тут происходит маппинг?
    }
}