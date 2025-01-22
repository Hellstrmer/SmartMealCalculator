using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMealCalculatorServer;
using SmartMealCalculatorServer.Helpers;
using System.Diagnostics;
using System.Xml.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartMealCalculatorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsDbContext _context;

        public IngredientsController(IngredientsDbContext context)
        {
            _context = context;
        }

        private static List<Ingredients> IngredientsList = new List<Ingredients>();
        [HttpGet] // GET /api/Ingredients
        public async Task<IActionResult> GetIngredients()
        {
            var IngredientsList = await _context.Ingredients.ToListAsync();

            var SortedList = IngredientsList.
                OrderByDescending(x => x.Created)
                .ThenBy(x => x.ProductName)
                .ThenBy(x => x.Brands)
                .ToList();

            return Ok(SortedList);

        }
        [HttpPost("AddIngredient")] 
        public async Task<IActionResult> AddIngredient(Ingredients ingredient)
        {
            Debug.WriteLine($"Received Ingredient: {ingredient?.ProductName}, {ingredient?.Brands}, {ingredient?.EnergyKcal100g}");
            if (ingredient == null || string.IsNullOrWhiteSpace(ingredient.ProductName) || string.IsNullOrWhiteSpace(ingredient.Brands))
            {
                return BadRequest("ProductName and Brands are required.");
            }
            try
            {
                var ing = await _context.Ingredients.FirstOrDefaultAsync(x =>
            x.ProductName == ingredient.ProductName
            && x.Brands == ingredient.Brands);
                if (ing != null)
                {
                    return BadRequest("Already added!");
                }
                _context.Ingredients.Add(ingredient);
                Debug.WriteLine("Adding ingredient to database...");
                await _context.SaveChangesAsync(); 
                Debug.WriteLine("Ingredient added successfully!");
                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while adding ingredient: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpPost("DeleteIngredient")]
        public async Task<IActionResult> DeleteIngredient([FromBody] string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.ProductName == name);

            if (ingredient == null)
            {
                return NotFound($"Ingredient with name '{name}' not found.");
            }
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return Ok($"Ingredient '{name}' deleted successfully.");
        }        
    }
}
