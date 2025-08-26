namespace MyWebGameShop.Models
{
    public class Recommendation
    {
        public string Description { get; set; }
        public string GameTitle { get; set; }
        public string GameVersion { get; set; }
        public float GameRate { get; set; }
        public string RecommendationComment { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
