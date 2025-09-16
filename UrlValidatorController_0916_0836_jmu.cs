// 代码生成时间: 2025-09-16 08:36:40
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace UrlValidatorApp
{
    // 控制器用于处理URL有效性验证的请求
    [ApiController]
    [Route("[controller]/[action]")]
    public class UrlValidatorController : ControllerBase
    {
        // POST方法用于接收要验证的URL
        [HttpPost]
        public IActionResult ValidateUrl(string url)
        {
            try
            {
                // 验证URL是否为null或空字符串
                if (string.IsNullOrWhiteSpace(url))
                {
                    return BadRequest("URL cannot be null or empty.");
                }

                // 使用Uri类检查URL格式是否正确
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    return BadRequest("Invalid URL format.");
                }

                // 尝试创建一个HttpWebRequest对象来验证URL是否可达
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // 如果响应状态码为200，则URL有效
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok("The URL is valid and reachable.");
                    }
                    else
                    {
                        return BadRequest($"URL is reachable but returned status code: {response.StatusCode}.");
                    }
                }
            }
            catch (UriFormatException)
            {
                return BadRequest("The URL format is incorrect.");
            }
            catch (WebException ex)
            {
                // 捕获网络异常并返回错误信息
                return BadRequest($"Error accessing the URL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // 捕获其他异常并返回错误信息
                return BadRequest($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}