// 代码生成时间: 2025-07-30 17:47:53
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;

// JsonDataConverter.cs
// 该类用于将JSON数据转换为不同的数据格式。
public class JsonDataConverter
{
    // 将JSON字符串转换为指定类型的实例
    public T ConvertJsonToType<T>(string jsonString) where T : class
    {
        // 尝试反序列化JSON字符串
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(jsonString, options);
        }
        catch (JsonException e)
        {
            // 处理JSON反序列化异常
            Console.WriteLine($"Error converting JSON to type {typeof(T).Name}: {e.Message}");
            return null;
        }
    }

    // 将对象转换为JSON字符串
    public async Task<string> ConvertTypeToJsonAsync<T>(T data) where T : class
    {
        // 尝试序列化对象
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.SerializeAsync(data, options);
        }
        catch (JsonException e)
        {
            // 处理JSON序列化异常
            Console.WriteLine($"Error converting type to JSON: {e.Message}");
            return null;
        }
    }

    // 从文件中读取JSON字符串并转换为指定类型
    public T ConvertJsonFromFileToType<T>(string filePath) where T : class
    {
        // 尝试读取和反序列化JSON文件
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            string jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonData, options);
        }
        catch (JsonException e)
        {
            // 处理JSON反序列化异常
            Console.WriteLine($"Error converting JSON from file to type {typeof(T).Name}: {e.Message}");
            return null;
        }
        catch (IOException e)
        {
            // 处理文件操作异常
            Console.WriteLine($"Error reading file: {e.Message}");
            return null;
        }
    }

    // 将对象写入JSON文件
    public void ConvertTypeToJsonFile<T>(T data, string filePath) where T : class
    {
        // 尝试序列化对象并写入文件
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            string jsonData = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonData);
        }
        catch (JsonException e)
        {
            // 处理JSON序列化异常
            Console.WriteLine($"Error converting type to JSON: {e.Message}");
        }
        catch (IOException e)
        {
            // 处理文件操作异常
            Console.WriteLine($"Error writing to file: {e.Message}");
        }
    }
}
