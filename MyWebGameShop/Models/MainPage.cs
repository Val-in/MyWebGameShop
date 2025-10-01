namespace MyWebGameShop.Models;

public class MainPage
{
    public int Id { get; set; }
    public string History { get; set; } = string.Empty;
    public string Facts { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    
    // FK на ShopInfo (nullable)
    public int? ShopInfoId { get; set; }
    public ShopInfo? ShopInfo { get; set; }
}