namespace MyWebGameShop.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Game Game { get; set; }
    public User User { get; set; }
    public Order Orders { get; set; }
}