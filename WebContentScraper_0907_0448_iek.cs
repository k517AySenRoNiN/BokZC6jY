// 代码生成时间: 2025-09-07 04:48:29
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;

// 网页内容抓取工具
public class WebContentScraper
{
    private readonly HttpClient _httpClient;

    // 构造函数
    public WebContentScraper()
    {
        _httpClient = new HttpClient();
    }

    // 异步方法，用于抓取网页内容
    public async Task<string> ScrapeWebContentAsync(string url)
    {
        try
        {
            // 发送HTTP GET请求获取网页内容
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            // 清理HTML内容
            content = CleanHtml(content);

            return content;
        }
        catch (HttpRequestException e)
        {
            // 网络请求异常处理
            Console.WriteLine($"Error fetching web content: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            // 其他异常处理
            Console.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }

    // 清理HTML内容的方法
    private string CleanHtml(string html)
    {
        // 使用正则表达式移除HTML标签和JavaScript代码
        string cleanedContent = Regex.Replace(html, "<.*?>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        cleanedContent = Regex.Replace(cleanedContent, @