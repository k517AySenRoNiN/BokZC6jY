// 代码生成时间: 2025-09-06 00:53:10
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

// JsonDataTransformer类用于将JSON数据从一个格式转换为另一个格式。
public class JsonDataTransformer
{
    // 将JSON字符串转换为C#对象。
    public T Deserialize<T>(string jsonData)
    {
        // 尝试反序列化JSON字符串为指定的类型。
        try
        {
            return JsonSerializer.Deserialize<T>(jsonData);
        }
        catch (JsonException ex)
        {
            // 处理反序列化时的错误。
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            throw;
        }
    }

    // 将C#对象序列化为JSON字符串。
    public string Serialize<T>(T data)
    {
        // 尝试序列化对象为JSON字符串。
        try
        {
            return JsonSerializer.Serialize(data);
        }
        catch (JsonException ex)
        {
            // 处理序列化时的错误。
            Console.WriteLine($"Error serializing data: {ex.Message}");
            throw;
        }
    }

    // 从文件中读取JSON数据，并将其反序列化为C#对象。
    public T LoadData<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} was not found.");
        }

        string jsonData = File.ReadAllText(filePath);
        return Deserialize<T>(jsonData);
    }

    // 将C#对象序列化为JSON字符串，并保存到文件。
    public void SaveData<T>(string filePath, T data)
    {
        string jsonData = Serialize(data);
        File.WriteAllText(filePath, jsonData);
    }
}
