namespace MyWebGameShop.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } 
    public string ImageUrl { get; set; } = string.Empty;
    
    // Ваша связь (оставляем)
    public int CategoryId { get; set; }
    public Category Category { get; set; } = new();
}