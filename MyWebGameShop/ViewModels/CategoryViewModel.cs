using MyWebGameShop.Enums;

namespace MyWebGameShop.ViewModels;

public class CategoryViewModel
{
    public string Name { get; set; } = string.Empty;
    public PlatformEnum Platform { get; set; }
    public GenreEnum Genre { get; set; }
    public List<GameDetailsViewModel> Games { get; set; } = null!;
}