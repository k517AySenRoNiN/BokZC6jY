// 代码生成时间: 2025-08-01 16:25:15
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// 定义数据清洗工具类
public class DataCleaner
{
    // 删除字符串中的非法字符
    public static string RemoveIllegalCharacters(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or whitespace.");
        }

        // 定义非法字符的正则表达式
        string illegalCharacters = @"[^\w.@-]+";

        // 替换非法字符为空字符串
        string cleanInput = Regex.Replace(input, illegalCharacters, "");

        return cleanInput;
    }

    // 填充缺失值
    public static Dictionary<string, string> FillMissingValues(Dictionary<string, string> data)
    {
        if (data == null)
        {
            throw new ArgumentNullException("Data cannot be null.");
        }

        foreach (var entry in data)
        {
            if (string.IsNullOrWhiteSpace(entry.Value))
            {
                entry.Value = "Unknown"; // 将缺失值替换为"Unknown"
            }
        }

        return data;
    }

    // 数据标准化
    public static string StandardizeData(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data cannot be null or whitespace.");
        }

        // 将数据转换为小写
        string standardizedData = data.ToLowerInvariant();

        return standardizedData;
    }

    // 读取文件并清洗数据
    public static List<string> CleanDataFromFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or whitespace.");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        try
        {
            // 读取文件内容
            string fileContent = File.ReadAllText(filePath);

            // 清洗文件内容
            string cleanContent = RemoveIllegalCharacters(fileContent);

            // 将清洗后的内容按行分割
            return new List<string>(cleanContent.Split(new[] { '
' }, StringSplitOptions.None));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            throw;
        }
    }
}
