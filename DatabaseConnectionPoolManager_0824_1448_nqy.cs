// 代码生成时间: 2025-08-24 14:48:16
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

// DatabaseConnectionPoolManager manages a pool of database connections.
public class DatabaseConnectionPoolManager
{
    private readonly List<DbConnection> pool;
    private readonly string connectionString;
    private readonly int maxPoolSize;
    private readonly Type connectionType;

    // Constructor for the DatabaseConnectionPoolManager.
    public DatabaseConnectionPoolManager(string connectionString, Type connectionType, int maxPoolSize)
    {
        this.connectionString = connectionString;
        this.maxPoolSize = maxPoolSize;
        this.connectionType = connectionType;
        this.pool = new List<DbConnection>();
    }

    // Method to get a connection from the pool.
    public DbConnection GetConnection()
    {
        lock (pool)
        {
            if (pool.Any())
            {
                var connection = pool.First();
                pool.RemoveAt(0);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;
            }
            else
            {
                return CreateConnection();
            }
        }
    }

    // Method to return a connection to the pool.
    public void ReturnConnection(DbConnection connection)
    {
        lock (pool)
        {
            if (pool.Count < maxPoolSize)
            {
                connection.Close();
                pool.Add(connection);
            }
            else
            {
                // Optionally dispose the connection if the pool is full.
                connection.Dispose();
            }
        }
    }

    // Method to create a new connection.
    private DbConnection CreateConnection()
    {
        var connection = (DbConnection)Activator.CreateInstance(connectionType);
        connection.ConnectionString = connectionString;
        try
        {
            connection.Open();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unable to open a database connection.", ex);
        }
        return connection;
    }

    // Method to clear the pool and dispose all connections.
    public void ClearPool()
    {
        lock (pool)
        {
            foreach (var connection in pool)
            {
                connection.Dispose();
            }
            pool.Clear();
        }
    }
}
