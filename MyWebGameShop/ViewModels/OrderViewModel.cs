namespace MyWebGameShop.ViewModels;

public class OrderViewModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItemViewModel> Items { get; set; }
}