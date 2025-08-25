namespace MyWebGameShop.ViewModels;

public class GameDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Genre { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
}