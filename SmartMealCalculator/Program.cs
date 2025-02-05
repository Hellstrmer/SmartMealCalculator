using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartMealCalculator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
Console.WriteLine("Hello from Blazor!"); // <-- Try this
/*builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
Console.WriteLine("Blazor-appen startar...");
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.Configuration["API_BASE_URL"];
Console.WriteLine($"ApiBaseUrl: {apiBaseUrl}");

if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl är inte konfigurerad. Kontrollera appsettings.json eller miljövariabeln API_BASE_URL.");
}



builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<OpenFoodFactsService>();

await builder.Build().RunAsync();
*/