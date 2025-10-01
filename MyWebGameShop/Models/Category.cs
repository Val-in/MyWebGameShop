using MyWebGameShop.Enums;

namespace MyWebGameShop.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PlatformEnum PlatformEnum { get; set; }
    public GenreEnum GenreEnum { get; set; }

    public List<Game> Games { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}