using MyWebGameShop.Models;

namespace MyWebGameShop.ViewModels;

public class MainPageViewModel
{
    public string History { get; set; }
    public string Facts { get; set; }
    public string Specialization { get; set; }

    public ShopInfoViewModel ShopInfo { get; set; }
}