using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IUserService
{
    Task AddUserAsync(User user); //Что передаём: готовый объект User, уже заполненный нужными данными (Name, Email и т.д.).
                             //Метод не нужно искать в базе, потому что данные уже есть в объекте.
    Task<User []> GetUsersAsync();
    Task<User> GetByLoginAsync(string userLogin); //Получаем объект по уникальному значению только у User, не нужна доп логика в сервисе
    Task<User> GetByEmailAsync(string userEmail);
    Task<User> GetByIdAsync(int userId);
}