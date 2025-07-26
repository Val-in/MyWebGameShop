namespace MyWebGameShop.Models;

public class Order
{
    public int Id { get; set; }

    public User UserId { get; set; } 
    
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Price { get; set; } 
}