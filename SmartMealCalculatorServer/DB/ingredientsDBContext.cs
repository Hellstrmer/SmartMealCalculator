using Microsoft.EntityFrameworkCore;

namespace SmartMealCalculatorServer.DB
{
    public class ingredientsDBContext : DbContext
    {
        public DbSet<string> Test {  get; set; }
    }
}
