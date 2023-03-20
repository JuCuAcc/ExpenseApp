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
                int rowsAffected = 0;

                // Open the connection
                connection.Open();

                while (true)
                {
                    // Show the main menu
                    Console.WriteLine("Main Menu:");
                    Console.WriteLine("1. View All Expenses");
                    Console.WriteLine("2. Add New Expense");
                    Console.WriteLine("3. Update Expense");
                    Console.WriteLine("4. Delete Expense");
                    Console.WriteLine("5. Exit");

                    // Get the user's choice
                    Console.Write("Enter your choice (1-5): ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            // View all expenses
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("All Expenses:");
                            Console.ResetColor();

                            string sql = "SELECT * FROM Expenses";
                            SqlCommand command = new SqlCommand(sql, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
                            }

                            reader.Close();
                            break;

                        case "2":
                            // Add a new expense
                            Console.Write("Description: ");
                            string description = Console.ReadLine();
                            Console.Write("Amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            sql = "INSERT INTO Expenses (Description, Amount) VALUES (@description, @amount)";
                            command = new SqlCommand(sql, connection);
                            command.Parameters.AddWithValue("@description", description);
                            command.Parameters.AddWithValue("@amount", amount);
                            rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("New expense added.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed to add new expense.");
                            }

                            Console.ResetColor();
                            break;

                        case "3":
                            // Update an expense
                            Console.Write("Enter expense ID to update: ");
                            int expenseId = int.Parse(Console.ReadLine());
                            Console.Write("New description: ");
                            string newDescription = Console.ReadLine();
                            Console.Write("New amount: ");
                            decimal newAmount = decimal.Parse(Console.ReadLine());

                            sql = "UPDATE Expenses SET Description = @description, Amount = @amount WHERE Id = @id";
                            command = new SqlCommand(sql, connection);
                            command.Parameters.AddWithValue("@description", newDescription);
                            command.Parameters.AddWithValue("@amount", newAmount);
                            command.Parameters.AddWithValue("@id", expenseId);
                            rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Expense updated.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed to update expense.");
                            }

                            Console.ResetColor();
                            break;

                        case "4":
                            // Delete an expense
                            Console.Write("Enter expense ID to delete: ");
                            expenseId = int.Parse(Console.ReadLine());

                            sql = "DELETE FROM Expenses WHERE Id = @id";
                            command = new SqlCommand(sql, connection);
                            command.Parameters.AddWithValue("@id", expenseId);
                            rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Expense deleted.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed to delete expense.");
                            }

                            Console.ResetColor();
                            break;

                        case "5":
                            // Exit the application
                            Console.Write("Are you sure you want to exit the application? (Y/N): ");
                            string exitChoice = Console.ReadLine();
                            if (exitChoice.ToLower() == "y")
                            {
                                Console.WriteLine("Exiting the application...");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Returning to the main menu...");
                            }

                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }

            }
        }
    }
}
