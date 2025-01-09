using System.Collections.Generic;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class SearchFile : Window
    {
        public FoodItem SelectedFood { get; private set; } // För att returnera vald maträtt

        public SearchFile()
        {
            InitializeComponent();
            LoadAllFoodItems();
        }

        private void LoadAllFoodItems()
        {
            var allItems = FoodDatabase.Select(""); // Ladda alla maträtter
            FoodDataGrid.ItemsSource = allItems;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim(); // Sökterm från användaren
            var filteredItems = FoodDatabase.Select(query);

            if (filteredItems.Count > 0)
            {
                FoodDataGrid.ItemsSource = filteredItems;
            }
            else
            {
                MessageBox.Show("Ingen mat hittades. Lägg till ny mat!");
            }
        }

        private void AddNewFoodButton_Click(object sender, RoutedEventArgs e)
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

                LoadAllFoodItems();
                MessageBox.Show($"Ny maträtt \"{addFoodWindow.NewFoodItem.Food}\" tillagd i databasen.");
            }
        }

        private void SelectFoodButton_Click(object sender, RoutedEventArgs e)
        {
            if (FoodDataGrid.SelectedItem is FoodItem selectedFood)
            {
                SelectedFood = selectedFood;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim();
            var filteredItems = FoodDatabase.Select(query);
            FoodDataGrid.ItemsSource = filteredItems;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}