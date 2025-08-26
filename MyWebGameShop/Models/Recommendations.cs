namespace MyWebGameShop.Models
{
    public class Recommendations
    {
        /// <summary>
        /// Recommendations – RecommendationList : 1 → many
        /// </summary>
        public int Id { get; set; }
        public string Description { get; set; }
        public RecommendationList RecommendationList { get; set; }
    }
}
