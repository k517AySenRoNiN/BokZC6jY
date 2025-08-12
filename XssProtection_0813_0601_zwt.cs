// 代码生成时间: 2025-08-13 06:01:26
using System;
using System.Web;

namespace XssProtection
{
    // 一个简单的XSS攻击防护类
    public static class XssProtector
    {
        // 清理输入，防止XSS攻击
        public static string CleanInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            // 创建一个HTTP上下文并获取请求对象
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            // 使用HTML编码对输入进行编码，防止XSS攻击
            return HttpUtility.HtmlEncode(input);
        }

        // 清理输出，防止XSS攻击
        public static string CleanOutput(string output)
        {
            if (string.IsNullOrEmpty(output))
            {
                return string.Empty;
            }

            // 使用HTML编码对输出进行编码，防止XSS攻击
            return HttpUtility.HtmlEncode(output);
        }
    }
}
