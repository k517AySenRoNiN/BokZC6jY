// 代码生成时间: 2025-08-05 08:54:08
 * 作者: [你的名字]
 * 日期: [当前日期]
 */
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceTesting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("性能测试开始...");

                // 设置目标URL
                string targetUrl = "http://example.com";

                // 创建Http客户端
                using (HttpClient client = new HttpClient())
                {
                    // 发送GET请求
                    HttpResponseMessage response = await client.GetAsync(targetUrl);

                    // 确保请求成功
                    if (response.IsSuccessStatusCode)
                    {
                        // 记录响应时间
                        Stopwatch stopwatch = Stopwatch.StartNew();

                        // 读取响应内容，以触发网络I/O
                        string content = await response.Content.ReadAsStringAsync();

                        // 停止计时器
                        stopwatch.Stop();

                        // 输出响应时间和内容长度
                        Console.WriteLine($"响应时间: {stopwatch.ElapsedMilliseconds}ms");
                        Console.WriteLine($"响应内容长度: {content.Length} 字节");
                    }
                    else
                    {
                        Console.WriteLine($"请求失败: 状态码 {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"发生异常: {ex.Message}");
            }
        }
    }
}
