using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebGameShop.Models
{
    public class UserSubscriptionInfo
    {
        public int Id { get; set; }
        public string SubscriptionType { get; set; }
        public string PaymentMethod { get; set; }
        public string SubscriptionStatus { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public string SubscriptionTier { get; set; }
        public string PaymentHistory { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int SubscriptionId { get; set; }
        public Subscriptions Subscription { get; set; }
    }
}
