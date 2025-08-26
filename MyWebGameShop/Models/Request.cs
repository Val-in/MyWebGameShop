using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebGameShop.Models;

[Table("Requests")]
public class Request
{
    [Key]
    public Guid Id { get; set; }
    public string UserAgent { get; set; }
    public  DateTime Date { get; set; }
    [Required]
    public  string  Url { get; set; }

    public bool IsLog { get; set; }
}