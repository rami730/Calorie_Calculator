using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calorie_Calculator.Classes
{
	internal class FoodItem
	{
		string food;
		int calories;
		int protein;
		int carbonhydrates;
		int fat;

		public FoodItem(string food, int calories, int protein, int carbonhydrates, int fat)
		{
			this.food = food;
			this.calories = calories;
			this.protein = protein;
			this.carbonhydrates = carbonhydrates;
			this.fat = fat;	
		}

		public override string ToString()
		{
			return food;
		}
	}
}
