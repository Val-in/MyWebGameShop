using MyWebGameShop.Enums;

namespace MyWebGameShop.Models
{
    public class Subscriptions
    {
        /// <summary>
        /// Subscriptions – SubscriptionUserInfos : 1 → many
        /// </summary>
        public int Id { get; set; }
        public SubscriptionEnum SubscriptionType { get; set; } 
        
        //Все связи пойдут через список в SubscriptionUserInfos
        public List<SubscriptionUserInfo> SubscriptionUserInfos { get; set; }
    }
}
