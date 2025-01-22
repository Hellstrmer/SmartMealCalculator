using System.Text.Json;

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using OpenFoodFactsCSharp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SmartMealCalculator
{
    public class OpenFoodFactsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://world.openfoodfacts.org/";

        public OpenFoodFactsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SearchResult> SearchProductAsync(string productName)
        {
            try
            {
                string languageCode = "sv";
                string fields = "code,product_name,brands,nutriments,countries,countries_tags,lang";
                string encodedName = Uri.EscapeDataString(productName);
                string queryString = $"{ApiUrl}/cgi/search.pl?" +
                    $"search_terms={encodedName}" +
                    $"&search_simple=1" +            
                    $"&action=process" +            
                    $"&json=1" +            
                    $"&fields={fields}" +            
                    $"&lc=sv" +                    
                    $"&cc=se" +            
                    $"&countries=Sweden";

                Console.WriteLine($"URL: {queryString}");

                var response = await _httpClient.GetAsync(queryString);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Response: {jsonResponse}");

                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(jsonResponse);
                Console.WriteLine($"Deserialized Count: {result?.Count}, Products null?: {result?.Products == null}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for product: {ex.Message}");
                throw;
            }
        }
    }
}
