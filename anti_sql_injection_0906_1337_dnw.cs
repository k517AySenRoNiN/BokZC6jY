// 代码生成时间: 2025-09-06 13:37:11
 * It includes error handling, comments, and adheres to C# best practices for maintainability and scalability.
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace AntiSqlInjectionExample
{
    public class DatabaseHelper
    {
        // Configuration for the database connection
        private readonly string _connectionString = "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;";

        // Method to establish a database connection
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Method to prevent SQL injection by using parameterized queries
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return dataTable;
        }

        // Example method to demonstrate parameterized query usage
        public DataTable GetProducts(int categoryId)
        {
            string query = "SELECT * FROM Products WHERE CategoryId = @CategoryId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId }
            };
            return ExecuteQuery(query, parameters);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            try
            {
                DataTable products = dbHelper.GetProducts(1);
                // Process the DataTable as needed
                foreach (DataRow row in products.Rows)
                {
                    Console.WriteLine(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}