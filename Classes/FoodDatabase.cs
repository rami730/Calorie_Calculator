using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Calorie_Calculator
{
	internal class FoodDatabase
	{
		static string databasePath = @"..\..\..\FoodDB.db";
		static string connectionString = $"Data Source={databasePath}";

		public static void Initialize()
		{
			//Creates the database file if it doesn't exist
			if (!File.Exists(databasePath))
				File.Create(databasePath).Dispose();

			//Connect to the database and create a table
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				//SQL command to create a table
				string createTableQuery =
					@"CREATE TABLE IF NOT EXISTS Foods 
					(Food STRING NOT NULL,
                    Calories INTEGER NOT NULL,
                    Protein INTEGER NOT NULL,
					Carbonhydrates INTEGER NOT NULL,
					Fat INTEGER NOT NULL);";

				//Execute the SQL command
				using (var command = new SQLiteCommand(createTableQuery, connection))
				{
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
		}
		public static void Insert(string food, int calories, int protein, int carbonhydrates, int fat)
		{
			//Connect to the database
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				//SQL command to insert
				string insertQuery =
					"INSERT INTO Foods (Food, Calories, Protein, Carbonhydrates, Fat) " +
					"VALUES (@Food, @Calories, @Protein, @Carbonhydrates, @Fat)";

				//Execute the SQL command
				using (var command = new SQLiteCommand(insertQuery, connection))
				{
					command.Parameters.AddWithValue("@Food", food);
					command.Parameters.AddWithValue("@Calories", calories);
					command.Parameters.AddWithValue("@Protein", protein);
					command.Parameters.AddWithValue("@Carbonhydrates", carbonhydrates);
					command.Parameters.AddWithValue("@Fat", fat);
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
		}
		public static void Delete(string food)
		{
			//Connect to the database
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				//SQL command to delete an entry
				string deleteQuery = "DELETE FROM Foods WHERE Food = @Food";

				//Execute the SQL command
				using (var command = new SQLiteCommand(deleteQuery, connection))
				{
					command.Parameters.AddWithValue("@Food", food);
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
		}
		public static List<FoodItem> Select(string dataToSelect)
		{
			List<FoodItem> foodList = new List<FoodItem>();

			//Connect to the database
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				//SQL command to select data
				string selectQuery = "SELECT * FROM Foods WHERE Food LIKE @Food COLLATE NOCASE";

				using (var command = new SQLiteCommand(selectQuery, connection))
				{
					command.Parameters.AddWithValue("@Food", dataToSelect + "%");
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Debug.WriteLine("FOOD SELECTED: " + reader["Food"]);
							foodList.Add(
								new FoodItem(
									reader["Food"].ToString(), 
									Convert.ToInt32(reader["Calories"]), 
									Convert.ToInt32(reader["Protein"]), 
									Convert.ToInt32(reader["Carbonhydrates"]), 
									Convert.ToInt32(reader["Fat"]))
								);
						}
					}
				}
				connection.Close();
				return foodList;
			}
		}

	}
}
