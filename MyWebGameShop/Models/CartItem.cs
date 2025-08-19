namespace MyWebGameShop.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public Order Orders { get; set; }
    public Order OrderId { get; set; }
}