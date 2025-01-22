using OpenFoodFactsCSharp.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace SmartMealCalculator
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public int? UseCount { get; set; }
        public string? ProductName { get; set; }
        public float? Amount { get; set; }
        public int? Portions { get; set; }
        public float? PerPortion { get; set; }
        public string Brands { get; set; }
        public float? Salt100g { get; set; }
        public float? Fat100g { get; set; }
        public float? Sugars100g { get; set; }
        public float? Carbohydrates100g { get; set; }
        public float? EnergyKcal100g { get; set; }
        public float? Proteins100g { get; set; }
        public DateTime? Created { get; set; }
    }
}