using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenFoodFactsCSharp.Models
{
    public class Product
    {
        [JsonProperty("languages_codes")]
        [NotMapped]
        public LanguagesCodes LanguagesCodes { get; set; }
        [JsonProperty("nutriments")]
        [NotMapped]
        public Nutriments Nutriments { get; set; } 
        public string Brands { get; set; }
        [JsonProperty("brands_debug_tags")]
        [NotMapped]
        public string[] BrandsDebugTags { get; set; }
        [JsonProperty("brands_tags")]
        [NotMapped]
        public string[] BrandsTags { get; set; }
        [JsonProperty("carbon_footprint_percent_of_known_ingredients")]
        [NotMapped]
        public string[] CountriesHierarchy { get; set; }
        [JsonProperty("countries_lc")]
        [NotMapped]
        public string CountriesLc { get; set; }
        [JsonProperty("countries_debug_tags")]
        [NotMapped]
        public string[] CountriesDebugTags { get; set; }
        [JsonProperty("countries_tags")]
        [NotMapped]
        public string[] CountriesTags { get; set; }
        [JsonProperty("correctors_tags")]
        [NotMapped]
        public string[] CorrectorsTags { get; set; }
        [JsonProperty("product_name")]
        [NotMapped]
        public string ProductName { get; set; }
    }
}
