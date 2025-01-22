using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OpenFoodFactsCSharp.Models;

namespace SmartMealCalculatorServer.Helpers
{
    public class IngredientsDbContext : DbContext
    {
        public DbSet<Ingredients> Ingredients { get; set; }

            public IngredientsDbContext(DbContextOptions<IngredientsDbContext> options) 
            : base(options) 
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredients>().HasData(

                new Ingredients { Id = 1, Code = "***", UseCount = 0, ProductName = "Pasta", Amount = 1000, Portions = 13, PerPortion = 13, Brands = "", Salt100g = 0, Fat100g = 0, Sugars100g = 0, Carbohydrates100g = 0, EnergyKcal100g = 360, Proteins100g = 0 }
                );
        }
    }
}
