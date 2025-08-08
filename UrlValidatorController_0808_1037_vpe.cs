// 代码生成时间: 2025-08-08 10:37:20
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// 定义一个控制器来处理URL验证的请求
[ApiController]
[Route("[controller]/[action]"])
public class UrlValidatorController : ControllerBase
{
    // 异步方法来验证URL的有效性
    [HttpGet]
    public async Task<IActionResult> ValidateUrl([FromQuery] string url)
    {
        try
        {
            if (string.IsNullOrEmpty(url))
            {
                // 如果URL为空或无效，则返回400错误
                return BadRequest("URL cannot be null or empty.");
            }

            // 使用HttpWebRequest来检查URL是否可达
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5000; // 设置超时为5秒

            using (var response = await request.GetResponseAsync())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // 如果响应状态码为200，则返回200成功
                    return Ok("URL is valid.");
                }
                else
                {
                    // 如果响应状态码不是200，则返回400错误
                    return BadRequest($"URL is not valid. Status code: {response.StatusCode}");
                }
            }
        }
        catch (WebException ex)
        {
            // 如果发生网络异常，则返回500错误
            return StatusCode(500, $"An error occurred while checking the URL: {ex.Message}");
        }
        catch (Exception ex)
        {
            // 捕获其他异常，并返回500错误
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}
