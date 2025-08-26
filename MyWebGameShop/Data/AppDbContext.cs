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
        Database.EnsureCreated();
    }

    /// Ссылки на таблицы
    public DbSet<Address> Addresses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<MainPage> MainPages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ShopInfo> ShopInfo { get; set; }
    public DbSet<Subscriptions> Subscriptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SubscriptionUserInfo> SubscriptionUserInfos { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Address>(e =>
        {
            e.ToTable("Addresses");
            e.HasKey(a => a.Id); //e.HasKey("Id"); // shadow-PK, когда можно не писать id в самой модели
            e.Property(a => a.Country).IsRequired();
            e.Property(a => a.City).IsRequired();
            e.Property(a => a.Street).IsRequired();
            e.Property(a => a.Building).IsRequired();
            e.Property(a => a.PostalCode).IsRequired();
            e.HasOne(a => a.User)           // навигационное свойство в Address
                .WithMany(u => u.Addresses)    // коллекция в User
                .HasForeignKey(a => a.UserId)  // FK
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<CartItem>(e =>
        {
            e.ToTable("CartItems");
            e.HasKey(ci => ci.Id);
            e.Property(ci => ci.Quantity).IsRequired();
            e.HasOne(ci => ci.Game) //каждый CartItem привязан к одной конкретной игре
                .WithOne() // игра не должна «знать» о CartItem
                .HasForeignKey<CartItem>(ci => ci.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ci => ci.Orders)
                .WithMany(o => o.CartItems)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("Categories");
            e.HasKey("Id"); // используем shadow-PK, если вашей модели нет Id
            e.Property<int>("Genre").IsRequired();
            e.Property<int>("Platform").IsRequired();
            
            // Один Category — много Games
            e.HasMany<Game>()
                .WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один Category — много Products
            e.HasMany<Product>()
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Contact>(e =>
        {
            e.ToTable("Contacts");
            e.HasKey(c => c.Id);
            e.Property(c => c.Email).IsRequired();
            e.Property(c => c.Phone).IsRequired();
        });
        
        modelBuilder.Entity<Feedback>(e =>
        {
            e.ToTable("Feedbacks");
            e.HasKey(c => c.Id);
            e.Property(c => c.From).IsRequired();
            e.Property(c => c.Text).IsRequired();
        });
        
        modelBuilder.Entity<Game>(e =>
        {
            e.ToTable("Games");
            e.HasKey(g => g.Id);
            e.Property(g => g.Title).IsRequired();
            e.Property(g => g.Description).IsRequired();
            e.Property(g => g.Price).IsRequired();
            
            // Много Games — одна Category 
            e.HasOne<Category>()
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CategoryId);
        });
        
        modelBuilder.Entity<MainPage>(e =>
        {
            e.ToTable("MainPages");
            e.HasKey("Id");
            e.HasOne<ShopInfo>()
                .WithMany()
                .HasForeignKey("ShopInfoId")
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("Orders");
            e.HasKey(o => o.Id);
            e.Property(o => o.OrderDate).IsRequired();
            e.Property(o => o.TotalAmount).IsRequired().HasColumnType("numeric(18,2)");
            e.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(o => o.CartItems)
                .WithOne(ci => ci.Orders)
                .HasForeignKey(ci => ci.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Products");
            e.HasKey(p => p.Id);
            
            // Много Products — одна Category 
            e.HasOne(p => p.Category) 
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // при удалении категории автоматически удаляются все продукты, привязанные к ней.
        });
        
        modelBuilder.Entity<Recommendation>(e =>
        {
            e.ToTable("Recommendations");
            e.HasKey("Id"); // shadow-PK
            e.Property("Description").IsRequired();
            e.Property("GameTitle").IsRequired();
            e.Property("GameVersion").IsRequired();
            e.Property<float>("GameRate").IsRequired();
            e.Property("RecommendationComment").IsRequired();

            e.HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.UserId);
        });
        
        modelBuilder.Entity<Request>(e =>
        {
            e.ToTable("Requests");
            e.HasKey(r => r.Id);
            e.Property(r => r.Date).IsRequired();
            e.Property(r => r.Url).IsRequired();
        });
        
        //для Role не создаем отдельный modelBuilder
        
        modelBuilder.Entity<ShopInfo>(e =>
        {
            e.ToTable("ShopInfos");
            e.HasKey("Id");
            e.HasOne<Contact>()
                .WithMany()
                .HasForeignKey("ContactId")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Address>()
                .WithMany()
                .HasForeignKey("AddressId")
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Subscriptions>(e =>
        {
            e.ToTable("Subscriptions");
            e.HasKey(s => s.Id);
        });
        
        modelBuilder.Entity<SubscriptionUserInfo>(e =>
        {
            e.ToTable("SubscriptionUserInfos");
            e.Property(ui => ui.PaymentMethod).IsRequired();
            e.Property(ui => ui.SubscriptionStatus).IsRequired();
            e.Property<DateTime>("SubscriptionStartDate").IsRequired();
            e.Property<DateTime>("SubscriptionEndDate").IsRequired();
            e.Property<DateTime>("LastPaymentDate").IsRequired();
            e.Property(ui => ui.SubscriptionTier).IsRequired();
            e.Property(ui => ui.PaymentHistory).IsRequired();
            e.ToTable("UserSubscriptionInfos");
            e.HasKey(u => u.Id);

            e.HasOne(u => u.User)
                .WithMany(u => u.SubscriptionUserInfos)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(u => u.Subscription)
                .WithMany(s => s.SubscriptionUserInfos)
                .HasForeignKey(u => u.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.Id);
                e.Property(u => u.UserName).IsRequired();
                e.Property(u => u.Login).IsRequired();
                e.Property(u => u.Password).IsRequired();
                e.Property(u => u.Email).IsRequired();
                e.Property(u => u.WalletBalance).IsRequired();
                
                e.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
       }
};
