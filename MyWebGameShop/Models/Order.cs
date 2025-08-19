namespace MyWebGameShop.Models;

public class Order
{
    public Guid Id { get; set; }

    public int UserId { get; set; } 
    
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Price { get; set; }
    public User User { get; set; } //внешний ключ?
    public List<CartItem> CartItems { get; set; }
}