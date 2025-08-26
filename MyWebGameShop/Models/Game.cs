namespace MyWebGameShop.Models;

public class Game
{
    /// <summary>
    /// Game – CartItems : 1 → many
    /// </summary>
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}