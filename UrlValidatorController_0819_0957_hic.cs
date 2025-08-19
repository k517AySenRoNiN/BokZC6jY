// 代码生成时间: 2025-08-19 09:57:08
using Microsoft.AspNetCore.Mvc;
using System;

namespace UrlValidatorApp.Controllers
{
    [ApiController]
    [Route("")]
    public class UrlValidatorController : ControllerBase
    {
        [HttpGet("validate-url")]
        public IActionResult ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                // 如果URL为空，则返回400错误
                return BadRequest("URL cannot be null or empty.");
            }

            try
            {
                // 使用Uri.TryCreate来验证URL格式是否正确
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    return BadRequest("Invalid URL format.");
                }

                // 这里可以添加更多的验证逻辑，例如检查URL是否可访问
                // 使用HttpClient检查URL是否可访问
                // using (var httpClient = new HttpClient())
                // {
                //     var response = await httpClient.GetAsync(uriResult);
                //     if (!response.IsSuccessStatusCode)
                //     {
                //         return BadRequest("URL is not accessible.");
                //     }
                // }

                // 如果URL有效，则返回200成功状态码
                return Ok("URL is valid.");
            }
            catch (Exception ex)
            {
                // 捕获并处理任何异常
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}