using MyWebGameShop.Enums;

namespace MyWebGameShop.Models
{
    public class Category
    {
        public bool FreeGame { get; set; }
        public bool PC {  get; set; }
        public bool Mobile { get; set; }
        public Genre Genre { get; set; }
        public List<Game> Games { get; set; }
    }
}
