using System.ComponentModel.DataAnnotations;

namespace MyWebGameShop.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int PostalCode { get; set; }
        
        public int UserId { get; set; }  // FK
        public User User { get; set; } //Связь с таблицей
    }
}
