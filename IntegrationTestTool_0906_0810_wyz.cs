// 代码生成时间: 2025-09-06 08:10:39
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

// 集成测试工具
public class IntegrationTestTool
{
    // 测试客户端
    private readonly WebApplicationFactory<Startup> _factory;

    public IntegrationTestTool()
    {
        // 初始化测试客户端工厂
        _factory = new WebApplicationFactory<Startup>();
    }

    // 异步发送GET请求
    public async Task<string> SendGetRequestAsync(string url)
    {
        try
        {
            // 创建客户端
            var client = _factory.CreateClient();

            // 发送GET请求
            var response = await client.GetAsync(url);

            // 检查响应状态码
            response.EnsureSuccessStatusCode();

            // 读取响应内容
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            // 错误处理
            Console.WriteLine($"Error sending GET request: {ex.Message}");
            throw;
        }
    }

    // 异步发送POST请求
    public async Task<string> SendPostRequestAsync(string url, object requestPayload)
    {
        try
        {
            // 创建客户端
            var client = _factory.CreateClient();

            // 序列化请求负载
            var content = new StringContent(JsonConvert.SerializeObject(requestPayload), System.Text.Encoding.UTF8, "application/json");

            // 发送POST请求
            var response = await client.PostAsync(url, content);

            // 检查响应状态码
            response.EnsureSuccessStatusCode();

            // 读取响应内容
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            // 错误处理
            Console.WriteLine($"Error sending POST request: {ex.Message}");
            throw;
        }
    }
}

// 测试类
public class IntegrationTestToolTests
{
    // 测试GET请求
    [Fact]
    public async Task TestGetRequest()
    {
        var testTool = new IntegrationTestTool();
        var response = await testTool.SendGetRequestAsync("/api/values");
        Assert.Equal("200 OK", response);
    }

    // 测试POST请求
    [Fact]
    public async Task TestPostRequest()
    {
        var testTool = new IntegrationTestTool();
        var requestPayload = new { Name = "Test User", Age = 30 };
        var response = await testTool.SendPostRequestAsync("/api/values", requestPayload);
        Assert.Equal("200 OK", response);
    }
}