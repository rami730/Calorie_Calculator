﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class SearchFile : Window
    {
        public FoodItem SelectedFood { get; private set; } // För att returnera vald maträtt
        private MainWindow mainWindow;
        public SearchFile(MainWindow _mainWindow)
        {
            InitializeComponent();
            LoadAllFoodItems();
            mainWindow = _mainWindow;
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

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
            if (FoodDataGrid.SelectedItem != null)
            {
				FoodItem selectedFood = FoodDataGrid.SelectedItem as FoodItem;

                mainWindow.proteinButton.Content = Convert.ToInt32(mainWindow.proteinButton.Content) + selectedFood.Protein;
				mainWindow.carbonHydratesButton.Content = Convert.ToInt32(mainWindow.carbonHydratesButton.Content) + selectedFood.Carbohydrates;
				mainWindow.fatButton.Content = Convert.ToInt32(mainWindow.fatButton.Content) + selectedFood.Fat;
			}
		}

		private void RemoveButton_Click(object sender, RoutedEventArgs e)
		{
			if (FoodDataGrid.SelectedItem != null)
			{
				FoodItem selectedFood = FoodDataGrid.SelectedItem as FoodItem;

				//Check that nutrients are not giving negative values
				if (selectedFood.Protein <= Convert.ToInt32(mainWindow.proteinButton.Content))
                {
                    mainWindow.proteinButton.Content = Convert.ToInt32(mainWindow.proteinButton.Content) - selectedFood.Protein;
                    mainWindow.carbonHydratesButton.Content = Convert.ToInt32(mainWindow.carbonHydratesButton.Content) - selectedFood.Carbohydrates;
                    mainWindow.fatButton.Content = Convert.ToInt32(mainWindow.fatButton.Content) - selectedFood.Fat;
                }
			}
		}
	}
}