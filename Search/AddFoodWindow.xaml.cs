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
            // Validera och skapa ny maträtt
            if (!string.IsNullOrWhiteSpace(FoodNameTextBox.Text) &&
                int.TryParse(CaloriesTextBox.Text, out int calories) &&
                int.TryParse(ProteinTextBox.Text, out int protein) &&
                int.TryParse(CarbohydratesTextBox.Text, out int carbohydrates) &&
                int.TryParse(FatTextBox.Text, out int fat))
            {
                NewFoodItem = new FoodItem(FoodNameTextBox.Text, calories, protein, carbohydrates, fat);
                this.Close();
            }
            else
            {
                MessageBox.Show("Vänligen fyll i alla fält korrekt.");
            }
        }

        public static void SetDailyGoal(int dailyCaloriesGoal)
        {
            // Funktion för att sätta användarens kalorimål.
            Debug.WriteLine($"Kalorimål satt till: {dailyCaloriesGoal}");
        }
    }
}
