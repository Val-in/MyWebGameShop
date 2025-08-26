using MyWebGameShop.Enums;

namespace MyWebGameShop.Models
{
    /// <summary>
    /// Category – Games : 1 → many
    /// Category – Products : 1 → many
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public Platform Platform { get; set; }
        public Genre Genre { get; set; }
        
        // связь 1 ко многим
        public List<Game> Games { get; set; }
        public List<Product> Products { get; set; }
    }
}
