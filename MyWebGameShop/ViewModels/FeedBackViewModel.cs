using System.ComponentModel.DataAnnotations;

namespace MyWebGameShop.ViewModels;

public class FeedBackViewModel
{
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(50, ErrorMessage = "Имя не должно быть длиннее 50 символов")]
    public string From { get; set; }

    [Required(ErrorMessage = "Введите текст отзыва")]
    [StringLength(500, ErrorMessage = "Отзыв не должен быть длиннее 500 символов")]
    public string Text { get; set; }
}