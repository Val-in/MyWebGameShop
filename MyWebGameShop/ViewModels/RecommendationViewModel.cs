namespace MyWebGameShop.ViewModels;

public class RecommendationViewModel
{
    public int Id { get; set; }  
    public string Description { get; init; } = string.Empty;
    public string GameTitle { get; init; } = string.Empty;
    public string GameVersion { get; init; } = string.Empty;
    public float GameRate { get; init; }
    public string RecommendationComment { get; init; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}