namespace Cosmetic_Finder.Common.Domain.Model
{
    public class Compose
    {
        public int Id { get; set; }
        public string ProductCompose { get; set; }

        public Compose(int id, string productCompose)
        {
            Id = id;
            ProductCompose = productCompose;
        }
    }
}
