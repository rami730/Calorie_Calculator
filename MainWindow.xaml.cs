using Calorie_Calculator;
using System.Diagnostics;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class MainWindow : Window
    {
        private int DailyCalorieGoal = 2000; // Standardvärde för kalorimål

        public MainWindow()
        {
            InitializeComponent();
            FoodDatabase.Initialize(); // Initiera databasen
        }

        private void SetCalorieGoal_Click(object sender, RoutedEventArgs e)
        {
            // Sätt dagligt kalorimål
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Ange ditt dagliga kalorimål:",
                "Kalorimål",
                DailyCalorieGoal.ToString());

            if (int.TryParse(input, out int newGoal) && newGoal > 0)
            {
                DailyCalorieGoal = newGoal;
                CalorieProgressBar.Maximum = DailyCalorieGoal;
                MessageBox.Show($"Ditt dagliga kalorimål är nu {DailyCalorieGoal} kcal.");
            }
            else
            {
                MessageBox.Show("Ogiltigt värde. Försök igen.");
            }
        }

        private void AddNewFood_Click(object sender, RoutedEventArgs e)
        {
            // Lägg till ny maträtt
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
            // Öppna SearchFile för att lägga till måltid
            var searchWindow = new SearchFile();
            searchWindow.ShowDialog();

            if (searchWindow.SelectedFood != null)
            {
                var selectedFood = searchWindow.SelectedFood;

                if (CalorieProgressBar.Value + selectedFood.Calories <= DailyCalorieGoal)
                {
                    CalorieProgressBar.Value += selectedFood.Calories;
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
