using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Calorie_Calculator
{
    public partial class SearchFile : Window
    {
        // Mock-data för matprodukter
        private List<FoodItem> FoodItems = new List<FoodItem>
        {
            new FoodItem { Name = "Äpple", Calories = 52, Protein = 0.3, Carbs = 14, Fat = 0.2 },
            new FoodItem { Name = "Kyckling", Calories = 239, Protein = 27, Carbs = 0, Fat = 14 },
            new FoodItem { Name = "Bröd", Calories = 265, Protein = 9, Carbs = 49, Fat = 3.2 },
        };

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

    public class FoodItem
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
    }
}
