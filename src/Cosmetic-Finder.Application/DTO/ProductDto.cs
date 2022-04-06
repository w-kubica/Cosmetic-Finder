namespace Cosmetic_Finder.Application.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Compose { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public int MainCategoryId { get; set; }
        public string Category { get; set; }
        public string NavigateUrl { get; set; }
        public string Picture { get; set; }
    }
}
