namespace SmartMealCalculatorServer
{
    public class Meal
    {
        private List<Ingredients> Ingredients {get; set; }
        private float? TotalCalories { get; set; }
        private float? TotalAmount {  get; set; }
        private float? TotalPortion {  get; set; }
        private float? TotalPerPortion { get; set; }

        public Meal(List<Ingredients> ingredients, float? totalCalories, float? totalAmount, float? totalPortion, float? totalPerPortion)
        {
            Ingredients = ingredients;
            TotalCalories = totalCalories;
            TotalAmount = totalAmount;
            TotalPortion = totalPortion;
            TotalPerPortion = totalPerPortion;
        }
    }
}
