using MyWebGameShop.Enums;

namespace MyWebGameShop.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } =string.Empty;
    public string LastName { get; set; } =string.Empty;
    public string UserAgent { get; set; } =string.Empty;
    public string Login { get; set; } =string.Empty;
    public string Password { get; set; } =string.Empty;
    public string Email { get; set; } =string.Empty;
    public decimal WalletBalance { get; set; }
    public DateTime Joined { get; set; }
    public RolesEnum Role { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<Address> Addresses { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();
    public List<SubscriptionUserInfo> SubscriptionUserInfos { get; set; } = new();
    public List<Recommendation> Recommendations { get; set; } = new();
}