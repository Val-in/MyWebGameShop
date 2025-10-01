using System.ComponentModel.DataAnnotations;
using MyWebGameShop.Enums;

namespace MyWebGameShop.ViewModels;

public class GameDetailsViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Название игры обязательно")]
    [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Описание игры обязательно")]
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Цена обязательна")]
    [Range(0, 10000, ErrorMessage = "Цена должна быть от 0 до 10000")]
    public decimal Price { get; set; }
    public GenreEnum Genre { get; set; }
    public PlatformEnum Platform { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}