using MyWebGameShop.Enums;

namespace MyWebGameShop.Models;

public class Subscriptions
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SubscriptionEnum SubscriptionType { get; set; }
    public int DurationMonths { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Связь с пользователями
    public List<SubscriptionUserInfo> SubscriptionUserInfos { get; set; } = new();
}