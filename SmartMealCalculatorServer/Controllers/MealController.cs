using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMealCalculatorServer.Helpers;

namespace SmartMealCalculatorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IngredientsDbContext _context;
        private static List<Ingredients> MealList = new List<Ingredients>();
        public MealController(IngredientsDbContext context) 
        {
            _context = context;
            //MealList = new List<Ingredients>();
        }

        private static List<Ingredients> FoundIngredientsList = new List<Ingredients>();
        [HttpGet("GetIngredients")]
        public async Task<IActionResult> GetIngredients([FromQuery] string name)
        {
            if (name == null|| string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Invalid");
            }
            var ing = await _context.Ingredients.Where(x =>
            x.ProductName.ToLower().Contains(name.ToLower()) ||
            x.Brands.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            var SortedList = ing.
                OrderByDescending(x => x.UseCount)
                .ThenBy(x => x.ProductName)
                .ThenBy(x => x.Brands)
                .ToList();

            if (SortedList.Count == 0)
            {
                return NotFound();
            }
            return Ok(SortedList);            
        }
        [HttpGet("UpdateIngredients")]
        public async Task<IActionResult> UpdateIngredients()
        {
            return Ok(MealList);
        }        
        //Add to List
        [HttpPost("AddIngredient")]
        public async Task<IActionResult> AddIngredient(Ingredients ingredients)
        {
            bool Changed = false;
            if (ingredients == null || string.IsNullOrWhiteSpace(ingredients.ProductName))
            {
                return BadRequest("Invalid Ingredient.");
            }
            for (int i = 0; i < MealList.Count; i++)
            {
                if (MealList[i].ProductName.ToLower().Contains(ingredients.ProductName.ToLower()))
                {
                    MealList[i] = ingredients;
                    Changed = true;
                    break;
                }
            }
            if (!Changed)
            {
                MealList.Add(ingredients);
            }
            return Ok(MealList);
        }
        //Add to Database
        [HttpPost("AddToDatabase")]
        public async Task<IActionResult> AddToDatabase(Ingredients ingredient)
        {
            if (ingredient == null || string.IsNullOrWhiteSpace(ingredient.ProductName))
            {
                return BadRequest("Invalid Ingredient.");
            }
            var ing = await _context.Ingredients.FirstOrDefaultAsync(x =>
            x.ProductName == ingredient.ProductName
            && x.Brands == ingredient.Brands);
            if (ing != null)
            {
                return BadRequest("Already added!");
            }
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return Ok(ingredient);
        }
        [HttpPost("DeleteIngredient")]
        public async Task<IActionResult> DeleteIngredient([FromBody] string name)
        {
            bool deleted = false;
            for (int i = 0;i < MealList.Count; i++)
            {
                if (MealList[i].ProductName.ToLower().Contains(name.ToLower()))
                {
                    MealList.Remove(MealList[i]);
                    deleted = true;
                    break;
                }
            }
            if (!deleted)
            {
                return NotFound($"Ingredient with name '{name}' not found.");
            } else
            {
                return Ok($"Ingredient '{name}' deleted successfully.");
            }
        }
        [HttpPost("EmptyRecord")]
        public async Task<IActionResult> EmptyRecord()
        {
            MealList.Clear();
            if (MealList.Count > 0)
            {
                return BadRequest("Empty record failed!");
            } else
            {
                return Ok();
            }
        }
        [HttpPost("UpdateDatabase")]
        public async Task<IActionResult> UpdateDataBase(Ingredients ingredients)
        {
            var DatabaseIngredient = await _context.Ingredients.FindAsync(ingredients.Id);
            if (DatabaseIngredient == null)
            {
                return NotFound();
            }
            _context.Entry(DatabaseIngredient).CurrentValues.SetValues(ingredients);    
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
