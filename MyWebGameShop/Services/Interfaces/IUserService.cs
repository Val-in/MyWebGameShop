using MyWebGameShop.Models;
using MyWebGameShop.Enums;

namespace MyWebGameShop.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByLoginAsync(string login);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(int id, User user);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> ValidatePasswordAsync(string login, string password);
    Task<bool> UpdateWalletBalanceAsync(int userId, decimal amount);
    Task<List<User>> GetUsersByRoleAsync(RolesEnum role);
    Task<bool> UpdateUserRoleAsync(int userId, RolesEnum role);
    Task<Dictionary<RolesEnum, int>> GetUsersCountByRoleAsync();

    // Алиасы для совместимости с контроллерами
    Task<User> AddUserAsync(User user);
    Task<bool> VerifyPasswordAsync(string email, string password);
}