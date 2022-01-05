using System.Collections.Generic;

namespace Cosmetic_Finder.Importer.Response
{
    public class ResponseProductsAdditionals
    {
        public List<Datum> Data { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string html { get; set; }
    }

}
