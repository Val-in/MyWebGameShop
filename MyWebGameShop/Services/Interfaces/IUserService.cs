using MyWebGameShop.Models;

namespace MyWebGameShop.Services.Interfaces;

public interface IUserService
{
    Task AddUser(User user);
    Task<User []> GetUsers();
    User GetByLogin(string login);
    
}