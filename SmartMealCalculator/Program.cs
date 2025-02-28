using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartMealCalculator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// L�s ApiBaseUrl och SignalRUrl fr�n konfigurationen
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.Configuration["API_BASE_URL"];
var signalRUrl = builder.Configuration["SignalRUrl"];

// Validera att ApiBaseUrl och SignalRUrl �r korrekt konfigurerade
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl �r inte konfigurerad. Kontrollera appsettings.json eller milj�variabeln API_BASE_URL.");
}

if (string.IsNullOrEmpty(signalRUrl))
{
    throw new InvalidOperationException("SignalRUrl �r inte konfigurerad. Kontrollera appsettings.json.");
}

builder.Services.AddSingleton(signalRUrl);

// Konfigurera HttpClient
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

// SignalR-tj�nsten
builder.Services.AddSingleton<SignalRService>(sp => new SignalRService(signalRUrl));

// Konfigurera OIDC-autentisering
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();