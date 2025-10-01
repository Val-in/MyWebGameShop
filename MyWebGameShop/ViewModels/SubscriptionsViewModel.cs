using MyWebGameShop.Enums;

namespace MyWebGameShop.ViewModels;

public class SubscriptionsViewModel
{
    public string PaymentMethod { get; set; } = string.Empty;
    public SubscriptionEnum SubscriptionType { get; set; }
    public string SubscriptionStatus { get; set; } = string.Empty;
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public string PaymentHistory { get; set; } = string.Empty;
}