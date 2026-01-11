using Newtonsoft.Json;
using System;

namespace OpenFoodFactsCSharp.Models
{
    public class Product
    {
        [JsonProperty("languages_codes")]
        public LanguagesCodes LanguagesCodes { get; set; }
        [JsonProperty("nutriments")]
        public Nutriments Nutriments { get; set; }
        public string Brands { get; set; }
        [JsonProperty("brands_debug_tags")]
        public string[] BrandsDebugTags { get; set; }
        [JsonProperty("brands_tags")]
        public string[] BrandsTags { get; set; }
        [JsonProperty("carbon_footprint_percent_of_known_ingredients")]
        public string[] CountriesHierarchy { get; set; }
        [JsonProperty("countries_lc")]
        public string CountriesLc { get; set; }
        [JsonProperty("countries_debug_tags")]
        public string[] CountriesDebugTags { get; set; }
        [JsonProperty("countries_tags")]
        public string[] CountriesTags { get; set; }
        [JsonProperty("correctors_tags")]
        public string[] CorrectorsTags { get; set; }
        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("_keywords")]
        public string[] Keywords { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
