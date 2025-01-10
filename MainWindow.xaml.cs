using Calorie_Calculator;
using System.Diagnostics;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class MainWindow : Window
    {
        private int dailyCalorieGoal = 2000;  //Default daily calorie goal

		public MainWindow()
        {
            InitializeComponent();
            FoodDatabase.Initialize(); //Initialize the databas
		}

        private void SetCalorieGoal_Click(object sender, RoutedEventArgs e)
        {
			//Set daily calorie goal
			var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Ange ditt dagliga kalorimål:",
                "Kalorimål",
                dailyCalorieGoal.ToString());

            if (int.TryParse(input, out int newGoal) && newGoal > 0)
            {
                dailyCalorieGoal = newGoal;
                calorieProgressBar.Maximum = dailyCalorieGoal;
                calorineGoalTextBlock.Text = "Kalorimål: " + dailyCalorieGoal;
                MessageBox.Show($"Ditt dagliga kalorimål är nu {dailyCalorieGoal} kcal.");

                proteinButton.Content = 0;
				carbonHydratesButton.Content = 0;
				fatButton.Content = 0;
                calorieProgress.Text = "Kalorier: 0";
                calorieProgressBar.Value = 0;
			}
            else
            {
                MessageBox.Show("Ogiltigt värde. Försök igen.");
            }
        }

        private void AddNewFood_Click(object sender, RoutedEventArgs e)
        {
			//Add new food item
			var addFoodWindow = new AddFoodWindow();
            addFoodWindow.ShowDialog();

            if (addFoodWindow.NewFoodItem != null)
            {
                FoodDatabase.Insert(
                    addFoodWindow.NewFoodItem.Food,
                    addFoodWindow.NewFoodItem.Calories,
                    addFoodWindow.NewFoodItem.Protein,
                    addFoodWindow.NewFoodItem.Carbohydrates,
                    addFoodWindow.NewFoodItem.Fat
                );
                MessageBox.Show($"Ny maträtt \"{addFoodWindow.NewFoodItem.Food}\" tillagd i databasen.");
            }
        }

        private void AddBreakfast_Click(object sender, RoutedEventArgs e)
        {
            AddMeal("Frukost");
        }

        private void AddLunch_Click(object sender, RoutedEventArgs e)
        {
            AddMeal("Lunch");
        }

        private void AddSnack_Click(object sender, RoutedEventArgs e)
        {
            AddMeal("Mellanmål");
        }

        private void AddDinner_Click(object sender, RoutedEventArgs e)
        {
            AddMeal("Middag");
        }

        private void AddMeal(string mealType)
        {
			//Open SearchFile to add a meal
			var searchWindow = new SearchFile(this, mealType);
            searchWindow.ShowDialog();

            if (searchWindow.SelectedFood != null)
            {
                var selectedFood = searchWindow.SelectedFood;

                if (calorieProgressBar.Value + selectedFood.Calories <= dailyCalorieGoal)
                {
                    calorieProgressBar.Value += selectedFood.Calories;
                    MessageBox.Show($"Lagt till {selectedFood.Food} ({selectedFood.Calories} kcal) till {mealType}.");
                }
                else
                {
                    MessageBox.Show("Varning: Kalorimålet har överskridits!");
                }
            }
        }
    }
}
