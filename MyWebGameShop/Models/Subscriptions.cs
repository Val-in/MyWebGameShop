namespace MyWebGameShop.Models
{
    public class Subscriptions
    {
        /// <summary>
        /// Subscriptions – UserSubscriptionInfos : 1 → many
        /// </summary>
        public int Id { get; set; }
        //Все связи пойдут через список в UserSubscriptionInfos
        public List<UserSubscriptionInfo> UserSubscriptionInfos { get; set; }
    }
}
