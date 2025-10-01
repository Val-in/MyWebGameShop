using MyWebGameShop.Enums;

namespace MyWebGameShop.ViewModels;

public class UserViewModel
{
    public string UserName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int WalletBalance { get; set; }
    
    public RolesEnum Role { get; set; }
    public List<CartItemViewModel> CartItems { get; set; } = new();
    public List<SubscriptionsViewModel> Subscriptions { get; set; } = new();
    public List<RecommendationViewModel> Recommendations { get; set; } = new();
}