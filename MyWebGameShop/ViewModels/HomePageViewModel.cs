namespace MyWebGameShop.ViewModels;

public class HomePageViewModel
{
    public List<GameDetailsViewModel> Games { get; set; } = new();
    public List<GameDetailsViewModel> Products { get; set; } = new();
}