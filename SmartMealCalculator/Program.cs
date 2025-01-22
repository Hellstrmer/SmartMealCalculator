using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartMealCalculator;
using System.Diagnostics;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"])
});

builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<OpenFoodFactsService>();

await builder.Build().RunAsync();
