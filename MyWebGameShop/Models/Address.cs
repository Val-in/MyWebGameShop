namespace MyWebGameShop.Models;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Building { get; set; }
    public int PostalCode { get; set; }
    
    // FK на User
    public int UserId { get; set; }
    public User User { get; set; } = new();
}