namespace MyWebGameShop.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItemViewModel> Items { get; set; } = null!;
}