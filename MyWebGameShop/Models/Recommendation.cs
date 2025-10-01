namespace MyWebGameShop.Models
{
    public class Recommendation
    {
        public int Id { get; set; } 
        public string Description { get; set; } = string.Empty;
        public string GameTitle { get; set; } = string.Empty;
        public string GameVersion { get; set; } = string.Empty;
        public float GameRate { get; set; }
        public string RecommendationComment { get; set; } = string.Empty;
        
        // Ваша связь (оставляем)
        public int UserId { get; set; }
        public User User { get; set; } = new();
    }
}