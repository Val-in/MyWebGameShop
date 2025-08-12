namespace MyWebGameShop.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int PostalCode { get; set; }
        public User User { get; set; }
        public User UserId { get; set; }
    }
}
