// 代码生成时间: 2025-08-11 10:42:01
using System;
using System.Data;
using System.Data.SqlClient;

namespace AntiSqlInjectionExample
{
    /// <summary>
    /// A class that demonstrates how to prevent SQL injection using parameterized queries.
    /// </summary>
    public class DatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Retrieves data from the database using a parameterized query to prevent SQL injection.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">Parameters for the SQL query.</param>
        /// <returns>A DataTable containing the query results.</returns>
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions here
                Console.WriteLine("An error occurred while executing the query: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions here
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return dataTable;
        }

        /// <summary>
        /// Adds a new user to the database using a parameterized query to prevent SQL injection.
        /// </summary>
        /// <param name="username">The username of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <returns>True if the user was added successfully, false otherwise.</returns>
        public bool AddUser(string username, string password)
        {
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password);";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(parameters);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions here
                Console.WriteLine("An error occurred while adding the user: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions here
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
