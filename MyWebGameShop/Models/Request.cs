namespace MyWebGameShop.Models;

public class Request
{
    public int Id { get; set; }
    public string UserAgent { get; set; } = string.Empty; 
    public DateTime Date { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Entry { get; set; } = string.Empty; 
    public bool IsLog { get; set; } = true; 
}