using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartMealCalculator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.Configuration["API_BASE_URL"];
Console.WriteLine($"ApiBaseUrl: {apiBaseUrl}");

if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl är inte konfigurerad. Kontrollera appsettings.json eller miljövariabeln API_BASE_URL.");
}


builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri(
            builder.HostEnvironment.IsDevelopment()
                ? "http://localhost:5099"  // Development
                : apiBaseUrl  // Production
        )
    };
    return httpClient;
});

builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<OpenFoodFactsService>();
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
