namespace Cosmetic_Finder.Common.Domain.Model
{
    public record Cosmetic
    {
        public int Id { get; set; }
        public string NavigateUrl { get; set; }
        public string Brand { get; set; }
        public string Caption { get; set; }
        public string Category { get; set; }
        public string Compose { get; set; }
        public double Price { get; set; }
    }
}
