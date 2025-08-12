namespace MyWebGameShop.Models;

public class Order
{
    public Guid Id { get; set; }

    public User UserId { get; set; } 
    
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Price { get; set; }
    public User User { get; set; } //в чем разница с UserId
    public List<CartItem> CartItems { get; set; }
}