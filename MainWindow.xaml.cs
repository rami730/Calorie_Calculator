using Calorie_Calculator.Classes;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calorie_Calculator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FoodDatabase.Initialize();
			FoodDatabase.Insert("Chicken", 100, 60, 21, 11);
			FoodDatabase.Insert("Chiicken", 100, 60, 21, 11);
			FoodDatabase.Insert("Chiiicken", 100, 60, 21, 11);

			List<FoodItem> foodsToSelect = new List<FoodItem>();
			foodsToSelect = FoodDatabase.Select("D");
			//FoodDatabase.Delete("Chicken");
			foreach (var item in foodsToSelect)
			{
				Debug.WriteLine(item);
			}
		}
	}
}