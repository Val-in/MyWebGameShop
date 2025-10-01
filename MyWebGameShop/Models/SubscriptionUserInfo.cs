namespace MyWebGameShop.Models;

public class SubscriptionUserInfo
{
    public int Id { get; set; }
    
    // FK на User
    public int UserId { get; set; }
    public User User { get; set; } = new();
    
    // FK на Subscription
    public int SubscriptionId { get; set; }
    public Subscriptions Subscription { get; set; } = new();
    
    public string PaymentMethod { get; set; } = string.Empty;
    public string SubscriptionStatus { get; set; } = string.Empty;
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public string? PaymentHistory { get; set; }
}