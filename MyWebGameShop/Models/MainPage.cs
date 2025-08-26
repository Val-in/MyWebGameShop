namespace MyWebGameShop.Models
{
    public class MainPage //контент для отображения на главной (слайдеры, баннеры, текст)
    {
        public int Id { get; set; }
        
        public string History { get; set; }

        public string Facts { get; set; }
        public string Specialization { get; set; }
        
        public int ShopInfoId { get; set; }
        public ShopInfo ShopInfo { get; set; }
        
    }
}
