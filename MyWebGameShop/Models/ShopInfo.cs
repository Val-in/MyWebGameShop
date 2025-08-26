namespace MyWebGameShop.Models
{
    public class ShopInfo //бизнес-инфа (юр. данные, контакты)
    {
        /// <summary>
        ///ShopInfo – Address / Contact : 1 → 1
        /// </summary>
        public int Id { get; set; }
        public string Owner { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
