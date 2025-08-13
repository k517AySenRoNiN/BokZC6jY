// 代码生成时间: 2025-08-13 16:45:21
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AntiSqlInjectionDemo
{
    /// <summary>
    /// Provides methods to prevent SQL injection attacks.
    /// </summary>
    public class AntiSqlInjectionService
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the AntiSqlInjectionService class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public AntiSqlInjectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a parameterized query to prevent SQL injection.
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="parameters">The parameters to bind to the query.</param>
        /// <returns>The result of the query execution.</returns>
        public DataTable ExecuteParameterizedQuery(string query, SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or whitespace.", nameof(query));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        return command.ExecuteReader().ToDataTable();
                    }
                }
                catch (SqlException ex)
                {
                    // Log the error and rethrow
                    throw new ApplicationException("SQL error occurred.", ex);
                }
            }
        }

        /// <summary>
        /// Sanitizes the input to prevent SQL injection.
        /// </summary>
        /// <param name="input">The input to sanitize.</param>
        /// <returns>The sanitized input.</returns>
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Use regular expressions to remove any potential SQL injection attempts
            // This is a simple example and may not cover all cases
            return Regex.Replace(input, @"[^a-zA-Z0-9\-_@#"