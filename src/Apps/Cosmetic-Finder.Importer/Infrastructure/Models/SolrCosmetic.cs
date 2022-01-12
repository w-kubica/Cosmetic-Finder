﻿using SolrNet.Attributes;
namespace Cosmetic_Finder.Importer.Infrastructure.Models
{
    public class SolrCosmetic
    {
        public const string CosmeticId = "id";
        public const string NavigateUrl = "url";
        public const string CosmeticBrand = "brand";
        public const string CosmeticCaption = "caption";
        public const string CosmeticCategory = "category";
        public const string CosmeticCompose = "compose";
        public const string CosmeticPrice = "price";

        [SolrUniqueKey(CosmeticId)]
        public int Id { get; set; }

        [SolrField(NavigateUrl)]
        public string Url { get; set; }

        [SolrField(CosmeticBrand)]
        public string Brand { get; set; }

        [SolrField(CosmeticCaption)]
        public string Caption { get; set; }

        [SolrField(CosmeticCategory)]
        public string Category { get; set; }

        [SolrField(CosmeticCompose)]
        public string Compose { get; set; }
        
        [SolrField(CosmeticPrice)]
        public double Price { get; set; }
    }
}
