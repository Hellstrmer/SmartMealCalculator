using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;

namespace SmartMealCalculator
{
    public class IngredientService
    {
        private readonly HttpClient _httpClient;

        public IngredientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Ingredients>> GetIngredientsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Ingredients>>("api/Ingredients");
                if (response == null)
                {
                    throw new Exception("No data returned from API.");
                }
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred: {ex.Message}");
                return new List<Ingredients>();
            }
        }       

        public async Task AddIngredientAsync(Ingredients ingredient)
        {
            Debug.WriteLine($"Sending ingredient: {ingredient.ProductName} - {ingredient.EnergyKcal100g} kcal");
            await _httpClient.PostAsJsonAsync("api/Ingredients/AddIngredient", ingredient);   
        }

        public async Task DeleteIngredientAsync(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ingredients/DeleteIngredient", name);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Deleted!");
            }
            else
            {
                Debug.WriteLine("Delete Failed.");
            }
        }

    }
}
