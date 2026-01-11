using Microsoft.EntityFrameworkCore;
using SmartMealCalculator;

namespace SmartMealCalculatorServer.Helpers
{
    public class GetOpenFoodFactsData
    {
        private readonly OpenFoodFactsService _openFoodFactsService;
        private readonly IngredientsDbContext _context;

        public GetOpenFoodFactsData(OpenFoodFactsService openFoodFactsService, IngredientsDbContext context)
        {
            _openFoodFactsService = openFoodFactsService;
            _context = context;
        }
        public async Task Search(string SearchName)
        {
            if (SearchName != null)
            {
                var FoundIngredients = new List<Ingredients>();
                bool isBarcode(string s) => s.All(char.IsDigit);
                if (!isBarcode(SearchName))
                {
                    var result = await _openFoodFactsService.SearchProductAsync(SearchName);
                    foreach (var product in result.Products)
                    {
                        var ing = new Ingredients()
                        {
                            Code = product.Code,
                            ProductName = product.ProductName,
                            Brands = product.Brands,
                            Salt100g = product.Nutriments.Salt100g,
                            Fat100g = product.Nutriments.Fat100g,
                            Sugars100g = product.Nutriments.Sugars100g,
                            Carbohydrates100g = product.Nutriments.Carbohydrates100g,
                            EnergyKcal100g = product.Nutriments.EnergyKcal100g,
                            Proteins100g = product.Nutriments.Proteins100g,
                            Created = DateTime.Now
                        };
                        FoundIngredients.Add(ing);
                        await AddToDB(ing);
                    }
                }
                else
                {
                    var result = await _openFoodFactsService.SearchBarcodeASync(SearchName);
                    if (result?.product != null)
                    {
                        var ing = new Ingredients()
                        {
                            Code = result.product.Code,
                            ProductName = result.product.ProductName,
                            Brands = result.product.Brands,
                            Salt100g = result.product.Nutriments.Salt100g,
                            Fat100g = result.product.Nutriments.Fat100g,
                            Sugars100g = result.product.Nutriments.Sugars100g,
                            Carbohydrates100g = result.product.Nutriments.Carbohydrates100g,
                            EnergyKcal100g = result.product.Nutriments.EnergyKcal100g,
                            Proteins100g = result.product.Nutriments.Proteins100g,
                            Created = DateTime.Now
                        };                       
                        FoundIngredients.Add(ing);
                        await AddToDB(ing);
                    }
                }
            }
        }

        private async Task AddToDB(Ingredients ingredient)
        {
            try
            {
                var ExistingIng = await _context.Ingredients
                    .FirstOrDefaultAsync(x => x.ProductName == ingredient.ProductName && x.Brands == ingredient.Brands);
                if (ExistingIng != null)
                    return;

                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid tillägg till databas: {ex.Message}");
            }
        }
    }
}
