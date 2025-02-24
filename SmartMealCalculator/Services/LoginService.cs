using System.Net.Http.Json;

namespace SmartMealCalculator.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterAsync()
        {
            var result = await _httpClient.PostAsJsonAsync(
                "register", new
                {
                    email,
                    password
                });
        }
    }
}
