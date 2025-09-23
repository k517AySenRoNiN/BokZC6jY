// 代码生成时间: 2025-09-23 19:46:49
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

/// <summary>
/// Manages a pool of database connections.
/// </summary>
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentBag<DbConnection> _connections;
    private readonly string _connectionString;
    private readonly int _maxPoolSize;
    private readonly Type _connectionType;

    /// <summary>
    /// Initializes a new instance of the DatabaseConnectionPoolManager class.
    /// </summary>
    /// <param name="connectionString">The database connection string.</param>
    /// <param name="maxPoolSize">The maximum number of connections in the pool.</param>
    public DatabaseConnectionPoolManager(string connectionString, int maxPoolSize)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        if (maxPoolSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxPoolSize), "Maximum pool size must be greater than zero.");
        }

        _connectionString = connectionString;
        _maxPoolSize = maxPoolSize;
        _connections = new ConcurrentBag<DbConnection>();
        _connectionType = Type.GetTypeFromProgID("System.Data.SqlClient.SqlConnection"); // For SQL Server, change as needed for other databases.
    }

    /// <summary>
    /// Gets a database connection from the pool.
    /// </summary>
    /// <returns>A database connection.</returns>
    public DbConnection GetConnection()
    {
        if (_connections.TryTake(out var connection))
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Close(); // Close the connection to ensure a fresh connection.
                // Reopen the connection to ensure it's valid.
                connection = CreateConnection();
            }
            else
            {
                // Reset the connection's state to open.
                connection = ResetConnection(connection);
            }

            return connection;
        }
        else
        {
            // If no connections are available, create a new one.
            return CreateConnection();
        }
    }

    /// <summary>
    /// Releases a database connection back to the pool.
    /// </summary>
    /// <param name="connection">The database connection to release.</param>
    public void ReleaseConnection(DbConnection connection)
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }

        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            // If the connection is closed or broken, it cannot be returned to the pool.
            // This should be logged and handled according to your error handling policy.
            // For simplicity, we are just disposing of it here.
            connection.Dispose();
            return;
        }

        // Reset the connection before returning it to the pool.
        connection = ResetConnection(connection);
        _connections.Add(connection);
    }

    private DbConnection CreateConnection()
    {
        var connection = (DbConnection)Activator.CreateInstance(_connectionType);
        connection.ConnectionString = _connectionString;
        connection.Open();
        return connection;
    }

    private DbConnection ResetConnection(DbConnection connection)
    {
        try
        {
            // Clear any pending transactions and command behaviors.
            // This is a simplified example, and additional steps may be required
            // depending on your specific requirements and database provider.
            connection.ClearAllPools();
            connection.ChangeDatabase(connection.Database);
        }
        catch (Exception ex)
        {
            // Handle exception, possibly dispose of the connection and create a new one.
            Console.WriteLine($"Error resetting connection: {ex.Message}");
            connection.Dispose();
            return CreateConnection();
        }

        return connection;
    }
}
