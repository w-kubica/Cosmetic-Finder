namespace Cosmetic_Finder.Infrastructure.Gateways.Response;

public class ResponseProductsAdditionals
{
    public List<Datum> Data { get; set; }
}

public class Datum
{
    public string Type { get; set; }
    public string Html { get; set; }
}
