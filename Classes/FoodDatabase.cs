using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace Calorie_Calculator
{
    internal class FoodDatabase
    {
        static string databasePath = @"..\..\..\FoodDB.db";
        static string connectionString = $"Data Source={databasePath}";

        public static void Initialize()
        {
            //Create the database file if it doesn't exist
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }

            //Connect to the database and create a table
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //SQL command to create a table
                string createTableQuery =
                    @"CREATE TABLE IF NOT EXISTS Foods 
                    (Food TEXT NOT NULL,
                    Calories INTEGER NOT NULL,
                    Protein INTEGER NOT NULL,
                    Carbohydrates INTEGER NOT NULL,
                    Fat INTEGER NOT NULL);";

                //Execute the SQL command
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void Insert(string food, int calories, int protein, int carbohydrates, int fat)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery =
                    "INSERT INTO Foods (Food, Calories, Protein, Carbohydrates, Fat) " +
                    "VALUES (@Food, @Calories, @Protein, @Carbohydrates, @Fat)";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Food", food);
                    command.Parameters.AddWithValue("@Calories", calories);
                    command.Parameters.AddWithValue("@Protein", protein);
                    command.Parameters.AddWithValue("@Carbohydrates", carbohydrates);
                    command.Parameters.AddWithValue("@Fat", fat);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void Delete(string food)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Foods WHERE Food = @Food";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Food", food);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static List<FoodItem> Select(string searchTerm)
        {
            List<FoodItem> foodList = new List<FoodItem>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Foods WHERE Food LIKE @Food COLLATE NOCASE";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Food", searchTerm + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foodList.Add(new FoodItem(
                                reader["Food"].ToString(),
                                Convert.ToInt32(reader["Calories"]),
                                Convert.ToInt32(reader["Protein"]),
                                Convert.ToInt32(reader["Carbohydrates"]),
                                Convert.ToInt32(reader["Fat"])
                            ));
                        }
                    }
                }
                connection.Close();
            }
            return foodList;
        }
    }
}

