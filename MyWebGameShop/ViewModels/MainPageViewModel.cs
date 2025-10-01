using MyWebGameShop.Models;

namespace MyWebGameShop.ViewModels;

public class MainPageViewModel
{
    public List<GameDetailsViewModel> FeaturedGames { get; set; } = new();
    public List<Product> FeaturedProducts { get; set; } = new();
    public string History { get; set; } = string.Empty;
    public string Facts { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}