using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Models;

namespace MyWebGameShop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Не вызываем EnsureCreated, чтобы не конфликтовать с SQL-скриптами.
    }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<MainPage> MainPages => Set<MainPage>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<ShopInfo> ShopInfos => Set<ShopInfo>();
    public DbSet<Subscriptions> Subscriptions => Set<Subscriptions>();
    public DbSet<SubscriptionUserInfo> SubscriptionUserInfos => Set<SubscriptionUserInfo>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Таблицы
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Game>().ToTable("Games");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<CartItem>().ToTable("CartItems");
        modelBuilder.Entity<Subscriptions>().ToTable("Subscriptions");
        modelBuilder.Entity<SubscriptionUserInfo>().ToTable("SubscriptionUserInfos");
        modelBuilder.Entity<Address>().ToTable("Addresses");
        modelBuilder.Entity<Contact>().ToTable("Contacts");
        modelBuilder.Entity<Feedback>().ToTable("Feedbacks"); 
        modelBuilder.Entity<ShopInfo>().ToTable("ShopInfos");
        modelBuilder.Entity<MainPage>().ToTable("MainPages");
        modelBuilder.Entity<Recommendation>().ToTable("Recommendations");
        modelBuilder.Entity<Request>().ToTable("Requests");

        // ===== User =====
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            e.Property(u => u.Login).IsRequired().HasMaxLength(50);
            e.Property(u => u.Password).IsRequired().HasMaxLength(100);
            e.Property(u => u.Email).IsRequired().HasMaxLength(100);
            e.Property(u => u.WalletBalance).IsRequired(); // int
            e.Property(u => u.Role).HasConversion<int>().IsRequired();

            e.HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(u => u.Addresses).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(u => u.CartItems).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(u => u.SubscriptionUserInfos).WithOne(su => su.User).HasForeignKey(su => su.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(u => u.Recommendations).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(u => u.Email).HasDatabaseName("IX_Users_Email");
            e.HasIndex(u => u.Login).HasDatabaseName("IX_Users_Login");
        });

        // ===== Category =====
        modelBuilder.Entity<Category>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired();
            e.Property(c => c.Description);
            e.HasMany(c => c.Games).WithOne(g => g.Category).HasForeignKey(g => g.CategoryId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== Game =====
        modelBuilder.Entity<Game>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(g => g.Title).IsRequired().HasMaxLength(200);
            e.Property(g => g.Description).IsRequired().HasMaxLength(1000);
            e.Property(g => g.Price).IsRequired().HasPrecision(18, 2);
            e.Property(g => g.ImageUrl).HasMaxLength(500);
            e.HasOne(g => g.Category).WithMany(c => c.Games).HasForeignKey(g => g.CategoryId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(g => g.Title).HasDatabaseName("IX_Games_Title");
        });

        // ===== Product =====
        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).IsRequired().HasMaxLength(200);
            e.Property(p => p.Description).HasMaxLength(1000);
            e.Property(p => p.Price).IsRequired().HasPrecision(18, 2);
            e.Property(p => p.Stock).IsRequired();
            e.Property(p => p.ImageUrl).HasMaxLength(500);
            e.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(p => p.Name).HasDatabaseName("IX_Products_Name");
        });

        // ===== Order =====
        modelBuilder.Entity<Order>(e =>
        {
            e.HasKey(o => o.Id);
            e.Property(o => o.OrderDate).IsRequired();
            e.Property(o => o.TotalAmount).IsRequired().HasPrecision(18, 2);
            e.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(o => o.CartItems).WithOne(ci => ci.Order).HasForeignKey(ci => ci.OrderId).OnDelete(DeleteBehavior.SetNull);
        });

        // ===== CartItem =====
        modelBuilder.Entity<CartItem>(e =>
        {
            e.HasKey(ci => ci.Id);
            e.Property(ci => ci.Quantity).IsRequired();

            e.HasOne(ci => ci.Game).WithMany().HasForeignKey(ci => ci.GameId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ci => ci.User).WithMany(u => u.CartItems).HasForeignKey(ci => ci.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ci => ci.Order).WithMany(o => o.CartItems).HasForeignKey(ci => ci.OrderId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
        });

        // ===== Subscriptions =====
        modelBuilder.Entity<Subscriptions>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Name).IsRequired().HasMaxLength(100);
            e.Property(s => s.Description).HasMaxLength(500);
            e.Property(s => s.Price).IsRequired().HasPrecision(18, 2);
            e.Property(s => s.SubscriptionType).HasConversion<int>().IsRequired();
            e.Property(s => s.DurationMonths).IsRequired();
            e.Property(s => s.CreatedAt).IsRequired();
            e.HasMany(s => s.SubscriptionUserInfos).WithOne(su => su.Subscription).HasForeignKey(su => su.SubscriptionId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== SubscriptionUserInfo =====
        modelBuilder.Entity<SubscriptionUserInfo>(e =>
        {
            e.HasKey(su => su.Id);
            e.Property(su => su.PaymentMethod).IsRequired().HasMaxLength(50);
            e.Property(su => su.SubscriptionStatus).IsRequired().HasMaxLength(20);
            e.Property(su => su.SubscriptionStartDate).IsRequired();
            e.Property(su => su.SubscriptionEndDate).IsRequired();
            e.Property(su => su.LastPaymentDate).IsRequired();
            e.Property(su => su.PaymentHistory).HasMaxLength(2000);
            e.HasOne(su => su.User).WithMany(u => u.SubscriptionUserInfos).HasForeignKey(su => su.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(su => su.Subscription).WithMany(s => s.SubscriptionUserInfos).HasForeignKey(su => su.SubscriptionId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== Address =====
        modelBuilder.Entity<Address>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Country).IsRequired().HasMaxLength(100);
            e.Property(a => a.City).IsRequired().HasMaxLength(100);
            e.Property(a => a.Street).IsRequired().HasMaxLength(200);
            e.Property(a => a.Building).IsRequired();
            e.Property(a => a.PostalCode).IsRequired();
            e.HasOne(a => a.User).WithMany(u => u.Addresses).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== Contact =====
        modelBuilder.Entity<Contact>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Email).IsRequired().HasMaxLength(100);
            e.Property(c => c.Phone).IsRequired().HasMaxLength(20);
            e.Property(c => c.Fax).HasMaxLength(20);
        });

        // ===== ShopInfo =====
        modelBuilder.Entity<ShopInfo>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Owner).IsRequired();
            e.HasOne(s => s.Contact).WithMany().HasForeignKey(s => s.ContactId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(s => s.Address).WithMany().HasForeignKey(s => s.AddressId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== MainPage =====
        modelBuilder.Entity<MainPage>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.History).IsRequired().HasMaxLength(2000);
            e.Property(m => m.Facts).IsRequired().HasMaxLength(2000);
            e.Property(m => m.Specialization).IsRequired().HasMaxLength(500);
            e.HasOne(m => m.ShopInfo).WithMany().HasForeignKey(m => m.ShopInfoId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
        });

        // ===== Feedback =====
        modelBuilder.Entity<Feedback>(e =>
        {
            e.HasKey(f => f.Id);
            e.Property(f => f.FromEmail).IsRequired().HasMaxLength(100);
            e.Property(f => f.Text).IsRequired().HasMaxLength(1000);
            e.Property(f => f.CreatedAt).IsRequired();
        });

        // ===== Recommendation =====
        modelBuilder.Entity<Recommendation>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Description).IsRequired().HasMaxLength(1000);
            e.Property(r => r.GameTitle).IsRequired().HasMaxLength(200);
            e.Property(r => r.GameVersion).IsRequired().HasMaxLength(50);
            e.Property(r => r.GameRate).IsRequired();
            e.Property(r => r.RecommendationComment).IsRequired().HasMaxLength(1000);
            e.HasOne(r => r.User).WithMany(u => u.Recommendations).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== Request (логи) =====
        modelBuilder.Entity<Request>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.UserAgent).HasMaxLength(500);
            e.Property(r => r.Date).IsRequired();
            e.Property(r => r.Url).IsRequired().HasMaxLength(500);
            e.Property(r => r.Entry).HasMaxLength(1000);
            e.Property(r => r.IsLog).IsRequired();
        });
    }
}
