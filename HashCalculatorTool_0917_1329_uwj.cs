// 代码生成时间: 2025-09-17 13:29:33
using System;
using System.Security.Cryptography;
using System.Text;

// 哈希值计算工具类
public class HashCalculatorTool
{
    // 计算字符串的哈希值
    public string CalculateHash(string input, HashAlgorithm algorithm)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
        }

        try
        {
            // 使用指定的哈希算法计算哈希值
            using (var hash = algorithm.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = hash.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        catch (CryptographicException e)
        {
            // 错误处理：哈希计算过程中的异常处理
            throw new InvalidOperationException("An error occurred during hash calculation.", e);
        }
        catch (Exception e)
        {
            // 错误处理：其他异常处理
            throw new Exception("An unexpected error occurred.", e);
        }
    }

    // 计算字符串的MD5哈希值
    public string CalculateMd5Hash(string input)
    {
        return CalculateHash(input, MD5.Create());
    }

    // 计算字符串的SHA256哈希值
    public string CalculateSha256Hash(string input)
    {
        return CalculateHash(input, SHA256.Create());
    }
}
