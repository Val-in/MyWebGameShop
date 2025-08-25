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
    public DbSet<LogEntry> Logs { get; set; }
    public DbSet<MainInformation> MainInformations { get; set; }
    public DbSet<MainPage> MainPages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<RecommendationList> RecommendationLists { get; set; }
    public DbSet<Recommendations> Recommendations { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ShopInfo> ShopInfo { get; set; }
    public DbSet<Subscriptions> Subscriptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder) //нужны ли связи для логгирования?*
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
            });

            modelBuilder.Entity<Game>(e =>
            {
                e.ToTable("Games");
                e.HasKey(g => g.Id);
                e.Property(g => g.Title).IsRequired();
                e.Property(g => g.Description).IsRequired();
                e.Property(g => g.Price).IsRequired();
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey("Id"); // используем shadow-PK, если вашей модели нет Id
                e.Property("FreeGame").IsRequired();
                e.Property("PC").IsRequired();
                e.Property("Mobile").IsRequired();
                e.Property<int>("Genre").IsRequired();
            });

            modelBuilder.Entity<Contact>(e =>
            {
                e.ToTable("Contacts");
                e.HasKey(c => c.Id);
                e.Property(c => c.Email).IsRequired();
                e.Property(c => c.Phone).IsRequired();
            });

            modelBuilder.Entity<Address>(e =>
            {
                e.ToTable("Addresses");
                e.HasKey("Id"); // shadow-PK
                e.Property(a => a.Country).IsRequired();
                e.Property(a => a.City).IsRequired();
                e.Property(a => a.Street).IsRequired();
                e.Property(a => a.Building).IsRequired();
                e.Property(a => a.PostalCode).IsRequired();
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(o => o.Id);
                e.Property(o => o.OrderDate).IsRequired();
                e.Property(o => o.TotalAmount).IsRequired().HasColumnType("numeric(18,2)");
                e.Property(o => o.Price).IsRequired().HasColumnType("numeric(18,2)");
                e.HasOne<User>()
                 .WithMany()
                 .HasForeignKey("UserId")
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<CartItem>(e =>
            {
                e.ToTable("CartItems");
                e.HasKey(ci => ci.Id);
                e.Property(ci => ci.Quantity).IsRequired();
                e.HasOne(ci => ci.Game)
                 .WithMany()
                 .HasForeignKey(ci => ci.GameId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(ci => ci.User)
                 .WithMany()
                 .HasForeignKey(ci => ci.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subscriptions>(e =>
            {
                e.ToTable("Subscriptions");
                e.HasKey("Id"); // shadow-PK
            });

            modelBuilder.Entity<UserSubscriptionInfo>(e =>
            {
                e.ToTable("UserSubscriptionInfos");
                e.HasKey("Id"); // shadow-PK
                e.Property(ui => ui.SubscriptionType).IsRequired();
                e.Property(ui => ui.PaymentMethod).IsRequired();
                e.Property(ui => ui.SubscriptionStatus).IsRequired();
                e.Property<DateTime>("SubscriptionStartDate").IsRequired();
                e.Property<DateTime>("SubscriptionEndDate").IsRequired();
                e.Property<DateTime>("LastPaymentDate").IsRequired();
                e.Property(ui => ui.SubscriptionTier).IsRequired();
                e.Property(ui => ui.PaymentHistory).IsRequired();
                e.HasOne<Subscriptions>()
                 .WithMany()
                 .HasForeignKey("SubscriptionsId")
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasOne<User>()
                 .WithMany()
                 .HasForeignKey("UserId")
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecommendationList>(e =>
            {
                e.ToTable("RecommendationLists");
                e.HasKey("Id"); // shadow-PK
                e.Property("Description").IsRequired();
                e.Property("GameTitle").IsRequired();
                e.Property("GameVersion").IsRequired();
                e.Property<float>("GameRate").IsRequired();
                e.Property("RecommendationComment").IsRequired();
                e.Property("User").IsRequired();
            });

            modelBuilder.Entity<Recommendations>(e =>
            {
                e.ToTable("Recommendations");
                e.HasKey("Id");
                e.Property("Description").IsRequired();
                e.HasOne<RecommendationList>()
                 .WithMany()
                 .HasForeignKey("RecommendationListId")
                 .OnDelete(DeleteBehavior.Cascade);
            });

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

            modelBuilder.Entity<MainInformation>(e =>
            {
                e.ToTable("MainInformations");
                e.HasKey("Id");
                e.Property(mi => mi.History).IsRequired();
                e.Property(mi => mi.Owner).IsRequired();
                e.Property(mi => mi.Facts).IsRequired();
                e.Property(mi => mi.Specialization).IsRequired();
            });

            modelBuilder.Entity<MainPage>(e =>
            {
                e.ToTable("MainPages");
                e.HasKey("Id");
                e.HasOne<ShopInfo>()
                 .WithMany()
                 .HasForeignKey("ShopInfoId")
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasOne<MainInformation>()
                 .WithMany()
                 .HasForeignKey("MainInformationId")
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Request>(e =>
            {
                e.ToTable("Requests");
                e.HasKey(r => r.Id);
                e.Property(r => r.Date).IsRequired();
                e.Property(r => r.Url).IsRequired();
            });

            modelBuilder.Entity<LogEntry>(e =>
            {
                e.ToTable("LogEntries");
                e.HasKey(l => l.Id);
                e.Property(l => l.Timestamp).IsRequired();
                e.Property(l => l.Url).IsRequired();
            });
        });

    }
};
