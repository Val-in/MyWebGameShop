using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IUserService
{
    Task AddUser(User user);
    Task<User []> GetUsers();
    User GetByLogin(string login);
    User GetByEmail(string email);
    User GetById(int id);
}