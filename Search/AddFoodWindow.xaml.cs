using System.Diagnostics;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class AddFoodWindow : Window
    {
        public FoodItem NewFoodItem { get; private set; }

        public AddFoodWindow()
        {
            InitializeComponent();
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
			//Validate and create a new food item
			if (!string.IsNullOrWhiteSpace(foodNameTextBox.Text) &&
                int.TryParse(caloriesTextBox.Text, out int calories) &&
                int.TryParse(proteinTextBox.Text, out int protein) &&
                int.TryParse(carbohydratesTextBox.Text, out int carbohydrates) &&
                int.TryParse(fatTextBox.Text, out int fat))
            {
                NewFoodItem = new FoodItem(foodNameTextBox.Text, calories, protein, carbohydrates, fat);
                this.Close();
            }
            else
            {
                MessageBox.Show("Vänligen fyll i alla fält korrekt.");
            }
        }

        public static void SetDailyGoal(int dailyCaloriesGoal)
        {
			//Method to set the user's calorie goal.
			Debug.WriteLine($"Kalorimål satt till: {dailyCaloriesGoal}");
        }
    }
}
