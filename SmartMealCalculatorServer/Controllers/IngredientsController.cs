using Microsoft.AspNetCore.Mvc;
using SmartMealCalculatorServer;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartMealCalculatorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private static List<Ingredients> IngredientsList = new List<Ingredients>();
        [HttpGet]
        public IActionResult GetIngredients()
        {
            return Ok(IngredientsList);
        }

        [HttpPost]
        public IActionResult AddIngredient(Ingredients ingredient)
        {

            if (ingredient == null || string.IsNullOrWhiteSpace(ingredient.Name))
            {
                return BadRequest("Invalid Ingredient.");
            }
            IngredientsList.Add(ingredient);
            return Ok(IngredientsList);
        }
    }
}
