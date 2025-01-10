using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class SearchFile : Window
    {
        public FoodItem SelectedFood { get; private set; } //To return the selected food item
        private MainWindow mainWindow;
        public SearchFile(MainWindow _mainWindow, string mealType)
        {
            InitializeComponent();
            LoadAllFoodItems();
            mainWindow = _mainWindow;
            mealTypeText.Text = mealType;
        }

        private void LoadAllFoodItems()
        {
            var allItems = FoodDatabase.Select(""); //Load all food items
            foodDataGrid.ItemsSource = allItems;
        }

        private void AddNewFoodButton_Click(object sender, RoutedEventArgs e)
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

                LoadAllFoodItems();
                MessageBox.Show($"Ny maträtt \"{addFoodWindow.NewFoodItem.Food}\" tillagd i databasen.");
            }
        }

        private void SelectFoodButton_Click(object sender, RoutedEventArgs e)
        {
            if (foodDataGrid.SelectedItem is FoodItem selectedFood)
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
			//Show or hide placeholder text based on the content of the TextBox
			if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                placeholderText.Visibility = Visibility.Collapsed; //Hide placeholder text
			}
            else
            {
                placeholderText.Visibility = Visibility.Visible; //Show placeholder text
			}

			//Dynamically update search results
			string query = searchTextBox.Text.Trim();
            var filteredItems = FoodDatabase.Select(query);
            foodDataGrid.ItemsSource = filteredItems;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			//Set daily calorie goal
			if (foodDataGrid.SelectedItem != null)
            {
				var input = Microsoft.VisualBasic.Interaction.InputBox("Ange antalet gram");
				if (int.TryParse(input, out int grams) && grams > 0)
				{
					FoodItem selectedFood = foodDataGrid.SelectedItem as FoodItem;
                    double weightMultiplier = grams / 100.0;
					mainWindow.proteinButton.Content = Convert.ToInt32(mainWindow.proteinButton.Content) + (selectedFood.Protein* weightMultiplier);
					mainWindow.carbonHydratesButton.Content = Convert.ToInt32(mainWindow.carbonHydratesButton.Content) + (selectedFood.Carbohydrates * weightMultiplier);
					mainWindow.fatButton.Content = Convert.ToInt32(mainWindow.fatButton.Content) + (selectedFood.Fat * weightMultiplier);
					mainWindow.calorieProgressBar.Value += (selectedFood.Calories * weightMultiplier);
                    mainWindow.calorieProgress.Text = "Kalorier: " + mainWindow.calorieProgressBar.Value;
				}
				else
				{
					MessageBox.Show("Ogiltigt värde. Försök igen.");
				}
			}
		}

		private void RemoveButton_Click(object sender, RoutedEventArgs e)
		{
			if (foodDataGrid.SelectedItem != null)
			{
				FoodItem selectedFood = foodDataGrid.SelectedItem as FoodItem;
				var input = Microsoft.VisualBasic.Interaction.InputBox("Ange antalet gram");

                if (int.TryParse(input, out int grams) && grams > 0)
                {
                    double weightMultiplier = grams / 100.0;

					//Check that nutrients are not giving negative values
					if (selectedFood.Calories * weightMultiplier <= mainWindow.calorieProgressBar.Value)
					{
						mainWindow.proteinButton.Content = Convert.ToInt32(mainWindow.proteinButton.Content) - (selectedFood.Protein * weightMultiplier);
						mainWindow.carbonHydratesButton.Content = Convert.ToInt32(mainWindow.carbonHydratesButton.Content) - (selectedFood.Carbohydrates * weightMultiplier);
						mainWindow.fatButton.Content = Convert.ToInt32(mainWindow.fatButton.Content) - (selectedFood.Fat * weightMultiplier);
						mainWindow.calorieProgressBar.Value -= (selectedFood.Calories * weightMultiplier);
						mainWindow.calorieProgress.Text = "Kalorier: " + mainWindow.calorieProgressBar.Value;
					}
				}
				else
				{
					MessageBox.Show("Ogiltigt värde. Försök igen.");
				}
			}
		}
	}
}