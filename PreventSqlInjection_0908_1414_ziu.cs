// 代码生成时间: 2025-09-08 14:14:42
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SqlInjectionPrevention
{
    // Database access class, designed to prevent SQL injection
    public class DataAccess
    {
        private string _connectionString;

        // Constructor to initialize the database connection string
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to securely Retrieve data from the database
        public DataTable GetData(string userId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Using parameterized query to prevent SQL injection
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@UserId", userId));

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine("General Exception: " + ex.Message);
            }

            return dataTable;
        }
    }

    // Main class to demonstrate the usage of the DataAccess class
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            DataAccess dataAccess = new DataAccess(connectionString);

            string userId = "InputUserId";
            try
            {
                DataTable dataTable = dataAccess.GetData(userId);

                // Displaying the retrieved data
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine("User ID: " + row[0] + ", Name: " + row[1] + ", Email: " + row[2]);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
