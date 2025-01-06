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
                var response = await _httpClient.GetFromJsonAsync<List<Ingredients>>("http://localhost:5099/api/Ingredients");
                if (response == null)
                {
                    throw new Exception("No data returned from API.");
                }
                return response;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.Error.WriteLine($"Error occurred: {ex.Message}");
                return new List<Ingredients>();
            }
        }

        /*public async Task<List<Ingredients>> GetIngredientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredients>>("http://localhost:5195/api/Ingredients");
        }*/
        public async Task AddIngredientAsync(Ingredients ingredient)
        {            
            Debug.WriteLine($"Sending ingredient: {ingredient.Name} - {ingredient.Calories} kcal");
            await _httpClient.PostAsJsonAsync("http://localhost:5099/api/Ingredients", ingredient);
        }

    }
}
