namespace MyWebGameShop.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string UserAgent { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int WalletBalance { get; set; }
    public DateTime Joined { get; set; }
    public Role Role { get; set; }
    public List<Order> Orders { get; set; }
    public List<Address> Addresses { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }
}