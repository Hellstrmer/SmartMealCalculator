using Newtonsoft.Json;
using System;

namespace OpenFoodFactsCSharp.Models
{
    public class ProductV2
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("brands")]
        public string Brands { get; set; }

        [JsonProperty("countries")]
        public string Countries { get; set; }

        [JsonProperty("countries_tags")]
        public string[] CountriesTags { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("nutriments")]
        public Nutriments Nutriments { get; set; }
    }
}
