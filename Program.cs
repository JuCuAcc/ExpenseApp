using System;
using System.Data.SqlClient;

namespace ExpenseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define connection string
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ExpenseAppDB;Trusted_Connection=True;MultipleActiveResultSets=true";

            // Create SQL connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SQL command to retrieve all expenses
                string sql = "SELECT * FROM Expenses";
                SqlCommand command = new SqlCommand(sql, connection);

                // Execute the command and retrieve the data
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("All Expenses:");
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
                }
                reader.Close();

                // Create a new expense
                Console.WriteLine("Add New Expense:");
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                // Create a SQL command to insert the new expense
                sql = "INSERT INTO Expenses (Description, Amount) VALUES (@description, @amount)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@amount", amount);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} row(s) affected.", rowsAffected);
            }

        }
    }
}
