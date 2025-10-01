namespace MyWebGameShop.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    
    // FK на Game
    public int GameId { get; set; }
    public Game Game { get; set; } = new();
    
    // FK на User
    public int UserId { get; set; }
    public User User { get; set; } = new();
    
    // FK на Order (NULLABLE!)
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}