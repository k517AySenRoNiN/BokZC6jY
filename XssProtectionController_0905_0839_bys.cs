// 代码生成时间: 2025-09-05 08:39:13
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

/// <summary>
/// 控制器用于提供XSS攻击防护功能。
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class XssProtectionController : ControllerBase
{
    /// <summary>
    /// 处理用户输入的字符串，以防止XSS攻击。
    /// </summary>
    /// <param name="input">用户输入的字符串</param>
    /// <returns>处理后的字符串</returns>
    [HttpGet]
    public IActionResult Protect(string input)
    {
        try
        {
            // 检查输入是否为空
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest("Input cannot be null or empty.");
            }

            // 使用正则表达式移除XSS攻击相关代码
            string safeInput = SanitizeInput(input);

            return Ok(safeInput);
        }
        catch (Exception ex)
        {
            // 错误处理，记录异常信息
            // 在实际应用中，应该使用日志记录器记录异常信息
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// 使用正则表达式清理字符串中的XSS攻击代码。
    /// </summary>
    /// <param name="input">需要清理的字符串</param>
    /// <returns>清理后的字符串</returns>
    private string SanitizeInput(string input)
    {
        // 正则表达式匹配常见的XSS攻击模式
        // 注意：这是一个简化的例子，实际应用中可能需要更复杂的规则
        string pattern = @"<(script|iframe|embed|object|style)[^>]*>.*?</\1>|<.*?javascript.*?>|<.*?expression.*?>";
        Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // 替换匹配到的XSS代码为空字符串
        string safeInput = regex.Replace(input, "");

        return safeInput;
    }
}