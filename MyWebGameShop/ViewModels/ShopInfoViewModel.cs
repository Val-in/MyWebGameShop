namespace MyWebGameShop.ViewModels;

public class ShopInfoViewModel
{
    public string Owner { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactFax { get; set; } = string.Empty;

    // Адресные данные прямо здесь
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Building { get; set; } 
    public int PostalCode { get; set; } 
}