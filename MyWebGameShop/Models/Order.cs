namespace MyWebGameShop.Models;

public class Order
{
    public Guid Id { get; set; } // UUID в PostgreSQL
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    
    // FK на User
    public int UserId { get; set; }
    public User User { get; set; } = new();
    
    // Связь с CartItems
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}