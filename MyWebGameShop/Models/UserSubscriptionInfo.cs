using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebGameShop.Models
{
    public class UserSubscriptionInfo
    {
        public string SubscriptionType { get; set; }
        public string PaymentMethod { get; set; }
        public string SubscriptionStatus { get; set; }
        public Date SubscriptionStartDate { get; set; }
        public Date SubscriptionEndDate { get; set; }
        public Date LastPaymentDate { get; set; }
        public string SubscriptionTier { get; set; }
        public string PaymentHistory { get; set; }
    }
}
