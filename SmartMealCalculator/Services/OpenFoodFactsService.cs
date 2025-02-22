using OpenFoodFactsCSharp.Models;
using Newtonsoft.Json;

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
        public async Task<SearchResultV2> SearchBarcodeASync(string barcode)
        {
            try
            {
                string fields = "code,product_name,brands,nutriments,countries,countries_tags,lang";
                string queryString = $"{ApiUrl}/api/v2/product/{barcode}?fields={fields}";

                Console.WriteLine($"URL: {queryString}");

                var response = await _httpClient.GetAsync(queryString);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Response: {jsonResponse}");

                var result = JsonConvert.DeserializeObject<SearchResultV2>(jsonResponse);

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
