using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace SmartMealCalculator
{
    public class MealService
    {
        private readonly HttpClient _httpClient;

        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ingredients>> GetIngredientsAsync( string name)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Ingredients>>($"api/Meal/GetIngredients?name={Uri.EscapeDataString(name)}");
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

        public async Task<List<Ingredients>> UpdateIngredientsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Ingredients>>("api/Meal/UpdateIngredients");
            return response;
        }
        //To List
        public async Task AddIngredientAsync(Ingredients ingredient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Meal/AddIngredient", ingredient);
        }
        //To Database
        public async Task AddToDatabaseAsync(Ingredients ingredient)
        {
            Debug.WriteLine($"Sending ingredient: {ingredient.ProductName} - {ingredient.EnergyKcal100g} kcal");
            await _httpClient.PostAsJsonAsync("api/Meal/AddToDatabase", ingredient);
        }
        public async Task DeleteIngredientAsync(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Meal/DeleteIngredient", name);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Deleted!");
            }
            else
            {
                Debug.WriteLine("Delete Failed.");
            }
        }
        public async Task EmptyRecordAsync()
        {
            var response = await _httpClient.PostAsync("api/Meal/EmptyRecord", null);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("record empty!");
            }
            else
            {
                Debug.WriteLine("Failed.");
            }
        }
        public async Task UpdateDatabaseAsync(Ingredients ingredient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Meal/UpdateDatabase", ingredient);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Updated!");
            }
            else
            {
                Debug.WriteLine("update Failed.");
            }        
        }
    }
}
