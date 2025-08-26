namespace MyWebGameShop.Models;

public class User
{
    /// <summary>
    /// User – Address : 1 → many
    /// User – Orders : 1 → many
    /// User – CartItems : 1 → many
    /// User – UserSubscriptionInfos : 1 → many
    /// User – Role : many → 1 (у каждого юзера одна роль, у роли — много пользователей)
    /// </summary>
    public int Id { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string UserAgent { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int WalletBalance { get; set; }
    public DateTime Joined { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    public List<Order> Orders { get; set; }
    public List<Address> Addresses { get; set; }
    //связь "один-ко-многим", внешний ключ (UserId) будет создан в таблице CartItems, а не в Users.
    //EF сам понимает, что связь "у User много CartItems", и создаст поле UserId в таблице CartItems.
    public List<CartItem> CartItems { get; set; }
    public List<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }
}