namespace MyWebGameShop.Models
{
    public class RecommendationList
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string GameTitle { get; set; }
        public string GameVersion { get; set; }
        public float GameRate { get; set; }
        public string RecommendationComment { get; set; }
        public string User { get; set; }
        
        public int RecommendationId { get; set; }  
        public Recommendations Recommendations { get; set; }
    }
}
