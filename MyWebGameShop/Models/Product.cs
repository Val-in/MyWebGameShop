using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebGameShop.Models;

public class Product
{
    [Key]
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; }  

    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; } 

    public int Stock { get; set; } 
    public string ImageUrl { get; set; }  

    // Навигационные свойства
    public int CategoryId { get; set; } 
    public Category Category { get; set; }  
}