using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Models;

namespace MyWebGameShop.Data;

/// <summary>
/// Класс контекста, предоставляющий доступ к сущностям базы данных
/// </summary>
public class AppDbContext : DbContext
{
    // Логика взаимодействия с таблицами в БД
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    /// Ссылки на таблицы
    public DbSet<Address> Addresses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<MainInformation> MainInformations { get; set; }
    public DbSet<MainPage> MainPages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<RecommendationList> RecommendationLists { get; set; }
    public DbSet<Recommendations> Recommendations { get; set; }
    public DbSet<ShopInfo> ShopInfo { get; set; }
    public DbSet<Subscriptions> Subscriptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(i => i.Id);
            e.Property(i => i.UserName).IsRequired();
        });
    }
}