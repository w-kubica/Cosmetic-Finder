namespace Cosmetic_Finder.Infrastructure.DTO.Rossmann;

public class ResponseProductsAdditionals
{
    public List<Datum> Data { get; set; }
}

public class Datum
{
    public string Type { get; set; }
    public string Html { get; set; }
}
