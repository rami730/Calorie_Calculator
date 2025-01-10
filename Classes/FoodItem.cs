namespace Calorie_Calculator
{
	public class FoodItem
	{
		public string Food { get; set; }
		public int Calories { get; set; }
		public int Protein { get; set; }
		public int Carbohydrates { get; set; }
		public int Fat { get; set; }

		public FoodItem(string food, int calories, int protein, int carbohydrates, int fat)
		{
			Food = food;
			Calories = calories;
			Protein = protein;
			Carbohydrates = carbohydrates;
			Fat = fat;
		}

		public override string ToString()
		{
			return $"{Food}: {Calories} kcal, {Protein}g Protein, {Carbohydrates}g Carbs, {Fat}g Fat";
		}
	}
}
