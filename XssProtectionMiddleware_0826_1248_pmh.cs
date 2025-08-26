// 代码生成时间: 2025-08-26 12:48:21
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.RegularExpressions;

namespace XssProtection
{
    public class XssProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public XssProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request is a GET request with query string parameters
            if (context.Request.Method == "GET" && context.Request.QueryString.HasValue)
            {
                // Sanitize the query string parameters
                var sanitizedQuery = SanitizeQueryString(context.Request.QueryString.Value);

                // Rewrite the original URL with sanitized query string
                var sanitizedUri = new UriBuilder(context.Request.Scheme, context.Request.Host.Host, context.Request.Host.Port, context.Request.PathBase + context.Request.Path)
               {
                    Query = sanitizedQuery
                };

                // Redirect to the sanitized URL
                context.Response.Redirect(sanitizedUri.ToString(), true);
            }
            else
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
        }

        // Sanitize the input to prevent XSS attacks
        private static string SanitizeQueryString(string queryString)
        {
            // Use Regex to remove any script tags or JavaScript event handlers
            return Regex.Replace(queryString, "<script(.*?)>(.*?)</script>|on[a-z]*=(.*?)('|")", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}

/*
 * Usage:
 * In the Startup.cs file, add the following code to the Configure method:
 * app.UseMiddleware<XssProtectionMiddleware>();
 */