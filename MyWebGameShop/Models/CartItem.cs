namespace MyWebGameShop.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }    
    public int GameId { get; set; }     
    public Game Game { get; set; }      

    // связь "один ко многим" → один User имеет много CartItem, тогда нужен внешний ключ.
    //CartItem "знает", к какому User он принадлежит → значит нужен UserId.
    public int UserId { get; set; }    
    public User User { get; set; }      

    public Guid OrderId { get; set; }  
    public Order Orders { get; set; }  
}