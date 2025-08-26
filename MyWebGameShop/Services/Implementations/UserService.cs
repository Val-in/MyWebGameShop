using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Data;
using MyWebGameShop.Models;
using MyWebGameShop.Services.Interfaces;

namespace MyWebGameShop.Services.Implementations;

/// <summary>
/// Операции с БД, которые необходимо производить, работают так же, как Repositories
/// </summary>
public class UserService : IUserService
{
    private readonly AppDbContext _context;

    // Метод-конструктор для инициализации
    public UserService(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddUserAsync(User user) //взаимодействие с базой данных — это потенциально длительная операция, поэтому уместно использовать асинхронные методы, возвращающие Task.
    {
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.AddAsync(user);
      
        // Сохранение изенений
        await _context.SaveChangesAsync();
    }
    
    public async Task<User[]> GetUsersAsync()
    {
        return await _context.Users.ToArrayAsync();
    }
    
    public async Task<User> GetByLoginAsync(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return await _context.Users
            .FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower());
    }

    public async Task<User> GetByEmailAsync(string userEmail)
    {
        if (string.IsNullOrWhiteSpace(userEmail))
            return null;

        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == userEmail.ToLower());
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }
}