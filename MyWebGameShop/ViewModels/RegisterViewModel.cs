using System.ComponentModel.DataAnnotations;
using MyWebGameShop.Enums; 

namespace MyWebGameShop.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    [Required]
    public string Login { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), MinLength(6)]
    public string Password { get; set; } = string.Empty;

    // Если хочешь задавать роль при регистрации — оставь; иначе задай дефолт в контроллере
    public RolesEnum Role { get; set; } = RolesEnum.User;
}