namespace MyWebGameShop.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Order Orders { get; set; }
    public Order OrderId { get; set; }
}