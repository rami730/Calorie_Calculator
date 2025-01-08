using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Calorie_Calculator;


namespace Calorie_Calculator
{
    public partial class SearchFile : Window
    {
        public SearchFile()
        {
            InitializeComponent();
            FoodDataGrid.ItemsSource = FoodItems; // Visa alla matprodukter som standard
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();
            var filteredItems = FoodItems.Where(f => f.Name.ToLower().Contains(query)).ToList();
            FoodDataGrid.ItemsSource = filteredItems;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigera tillbaka
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Avbryt och stäng fönstret
            this.Close();
        }
    }
}
