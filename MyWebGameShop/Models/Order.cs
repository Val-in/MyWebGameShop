namespace MyWebGameShop.Models;

public class Order
{
    /// <summary>
    /// Order – CartItems : 1 → many
    /// </summary>
    public Guid Id { get; set; }
    
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }
    
    public int UserId { get; set; }  
    public User User { get; set; } 
    public List<CartItem> CartItems { get; set; }
}