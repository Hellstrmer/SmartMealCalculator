using OpenFoodFactsCSharp.Models;

namespace OpenFoodFactsCSharp.Models
{
    public class SearchResult
    {
        public int Count { get; set; }       // Antal produkter som matchar sökningen
        public List<Product> Products { get; set; }  // Lista med de produkter som returneras
        
    }

}
