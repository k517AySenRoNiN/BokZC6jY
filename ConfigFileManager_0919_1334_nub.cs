// 代码生成时间: 2025-09-19 13:34:16
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

// ConfigFileManager is responsible for managing application configuration files.
public class ConfigFileManager
{
    private IConfigurationRoot _configuration;
    private readonly string _configFilePath;

    // Constructor that initializes the configuration manager with a specified configuration file path.
    public ConfigFileManager(string configFilePath)
    {
        _configFilePath = configFilePath;
        LoadConfiguration();
    }

    // Loads the configuration from the specified file path.
    private void LoadConfiguration()
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_configFilePath, optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during configuration loading.
            Console.WriteLine($"Error loading configuration: {ex.Message}");
            throw;
        }
    }

    // Gets a value from the configuration.
    public string GetValue(string key)
    {
        if (_configuration == null)
        {
            throw new InvalidOperationException("Configuration has not been loaded.");
        }

        return _configuration[key];
    }

    // Updates a value in the configuration file and saves changes.
    public void UpdateValue(string key, string value)
    {
        if (_configuration == null)
        {
            throw new InvalidOperationException("Configuration has not been loaded.");
        }

        try
        {
            // Load the JSON file into a dictionary.
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(_configFilePath));
            if (data == null) data = new Dictionary<string, string>();

            // Update the value.
            data[key] = value;

            // Save the changes back to the file.
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_configFilePath, JsonSerializer.Serialize(data, options));
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the update process.
            Console.WriteLine($"Error updating configuration: {ex.Message}");
            throw;
        }
    }

    // Reloads the configuration from the file.
    public void ReloadConfiguration()
    {
        LoadConfiguration();
    }
}
