namespace MyWebGameShop.ViewModels;

public class ShopInfoViewModel
{
    public string Owner { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public string ContactFax { get; set; }

    // Адресные данные прямо здесь
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int Building { get; set; }
    public int PostalCode { get; set; }
}