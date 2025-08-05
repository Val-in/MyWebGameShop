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
    public async Task AddUser(User user) //взаимодействие с базой данных — это потенциально длительная операция, поэтому уместно использовать асинхронные методы, возвращающие Task.
    {
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.AddAsync(user);
      
        // Сохранение изенений
        await _context.SaveChangesAsync();
    }
    
    public async Task<User[]> GetUsers()
    {
        // Получим всех активных пользователей
        return await _context.Set<User>().ToArrayAsync();
    }
}