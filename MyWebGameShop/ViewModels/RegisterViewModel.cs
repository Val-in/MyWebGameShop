using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebGameShop.Controllers;
using MyWebGameShop.Models;

namespace MyWebGameShop.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}