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
        //Database.EnsureDeleted(); автоматическое удаление и создание
        //Database.EnsureCreated();
    }

    /// Ссылки на таблицы
    public DbSet<Address> Addresses { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<MainInformation> MainInformations { get; set; }
    public DbSet<MainPage> MainPages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<RecommendationList> RecommendationLists { get; set; }
    public DbSet<Recommendations> Recommendations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ShopInfo> ShopInfo { get; set; }
    public DbSet<Subscriptions> Subscriptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(e =>
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.Id);
                e.Property(u => u.UserName).IsRequired();
                e.Property(u => u.Login).IsRequired();
                e.Property(u => u.Password).IsRequired();
                e.Property(u => u.Email).IsRequired();
                e.Property(u => u.WalletBalance).IsRequired();

                // User -> Orders (1 ко многим)
                e.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // User -> Addresses (1 ко многим)
                e.HasMany(u => u.Addresses)
                    .WithOne(a => a.User)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // User -> CartItems (1 ко многим)
                e.HasMany(u => u.CartItems)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Order -> CartItems
            modelBuilder.Entity<Order>(e =>
            {
                e.HasMany(o => o.CartItems)
                    .WithOne(ci => ci.Orders)
                    .HasForeignKey(ci => ci.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Game -> Category (многие к одному)
            modelBuilder.Entity<Game>(e =>
            {
                e.HasOne(g => g.Category)
                    .WithMany(c => c.Games)
                    .HasForeignKey(g => g.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // RecommendationList -> Recommendations (1 ко многим)
            modelBuilder.Entity<RecommendationList>(e =>
            {
                e.HasMany(rl => rl.Recommendations)
                    .WithOne(r => r.RecommendationList)
                    .HasForeignKey(r => r.RecommendationListId);
            });

            // User -> Subscriptions (многие-ко-многим через UserSubscriptionInfo)
            modelBuilder.Entity<UserSubscriptionInfo>()
                .HasKey(usi => new { usi.UserId, usi.SubscriptionId });

            modelBuilder.Entity<UserSubscriptionInfo>()
                .HasOne(usi => usi.User)
                .WithMany(u => u.UserSubscriptionInfos)
                .HasForeignKey(usi => usi.UserId);

            modelBuilder.Entity<UserSubscriptionInfo>()
                .HasOne(usi => usi.Subscription)
                .WithMany(s => s.UserSubscriptionInfos)
                .HasForeignKey(usi => usi.SubscriptionId);
        });

    }
};