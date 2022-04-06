namespace Cosmetic_Finder.Infrastructure.DTO;

public class ComposeDto
{
    public int Id { get; set; }
    public string ProductCompose { get; set; }

    public ComposeDto(int id, string productCompose)
    {
        Id = id;
        ProductCompose = productCompose;
    }
}
