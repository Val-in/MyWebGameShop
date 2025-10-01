namespace MyWebGameShop.Models;

public class Feedback
{
    public int Id { get; set; }
    public string FromEmail { get; set; } = string.Empty; // ✅ ИСПРАВЛЕНО: было From
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now; // ✅ ДОБАВЛЕНО
}