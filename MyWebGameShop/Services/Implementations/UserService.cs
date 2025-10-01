using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Enums;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
        => await _context.Users.ToListAsync();

    public async Task<User?> GetUserByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task<User?> GetUserByLoginAsync(string login)
        => await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task<User?> GetUserByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Алиас
    public Task<User> AddUserAsync(User user) => CreateUserAsync(user);

    public async Task<User?> UpdateUserAsync(int id, User user)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null) return null;

        existingUser.UserName = user.UserName;
        existingUser.Login = user.Login;
        existingUser.Email = user.Email;
        existingUser.WalletBalance = user.WalletBalance;
        existingUser.Role = user.Role;

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidatePasswordAsync(string login, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        return user != null && user.Password == password; // TODO: хеширование
    }

    // Алиас (проверка по email)
    public async Task<bool> VerifyPasswordAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user != null && user.Password == password; // TODO: хеширование
    }

    public async Task<bool> UpdateWalletBalanceAsync(int userId, decimal amount)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.WalletBalance += (int)Math.Round(amount);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetUsersByRoleAsync(RolesEnum role)
        => await _context.Users.Where(u => u.Role == role).ToListAsync();

    public async Task<bool> UpdateUserRoleAsync(int userId, RolesEnum role)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Role = role;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Dictionary<RolesEnum, int>> GetUsersCountByRoleAsync()
    {
        return await _context.Users
            .GroupBy(u => u.Role)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }
}
