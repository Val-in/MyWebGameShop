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
    private readonly List < User > _users = new List < User > ();

    public UserService() //как мы правильно должны заносить данные, когда у нас есть БД?
    {
        _users.Add(new User()
        {
            Id = Guid.NewGuid(),
            UserName = "Иван",
            LastName = "Иванов",
            Email = "ivan@gmail.com",
            Password = "11111122222qq",
            Login = "ivanov",
            Role = new Role()
            {
                Id = 1,
                RoleName = "Пользователь"
            }
        });

        _users.Add(new User()
        {
            Id = Guid.NewGuid(),
            UserName = "Максим",
            LastName = "Максимов",
            Email = "maksim@gmail.com",
            Password = "11",
            Login = "maxim",
            Role = new Role()
            {
                Id = 2,
                RoleName = "Администратор"
            }
        });
    }

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
    
    public User GetByLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return _users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
    }
}