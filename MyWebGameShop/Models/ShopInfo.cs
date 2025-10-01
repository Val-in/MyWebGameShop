namespace MyWebGameShop.Models;

public class ShopInfo
{
    public int Id { get; set; }
    public string Owner { get; set; } = string.Empty;
    
    // FK на Contact
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = new();

    // FK на Address
    public int AddressId { get; set; }
    public Address Address { get; set; } = new();
}