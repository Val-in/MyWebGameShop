using MyWebGameShop.Enums;

namespace MyWebGameShop.ViewModels;

public class UserViewModel
{
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int WalletBalance { get; set; }
    
    public RolesEnum Role { get; set; }
    public List<CartItemViewModel> CartItems { get; set; }
    public List<SubscriptionsViewModel> Subscriptions { get; set; }
    public List<RecommendationViewModel> Recommendations { get; set; }
}