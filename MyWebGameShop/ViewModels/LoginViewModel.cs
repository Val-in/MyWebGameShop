using System.ComponentModel.DataAnnotations;

namespace MyWebGameShop.ViewModels;

public class LoginViewModel
{
        [Required, EmailAddress]
        public string Email { get; set; } //зачем где-то надо атрибуты, а где-то не надо?

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
}