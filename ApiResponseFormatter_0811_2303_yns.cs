// 代码生成时间: 2025-08-11 23:03:42
using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ApiResponseFormatter 类用于格式化 API 响应。
public class ApiResponseFormatter
{
    // 私有构造函数，防止外部实例化。
    private ApiResponseFormatter() {}

    // 格式化成功的 API 响应。
    public static IActionResult FormatSuccessResponse(object data = null, string message = "Success")
    {
        var response = new
        {
            Success = true,
            Message = message,
            Data = data
        };

        return new OkObjectResult(response);
    }

    // 格式化失败的 API 响应。
    public static IActionResult FormatErrorResponse(int statusCode, string message = "An error occurred")
    {
        var response = new
        {
            Success = false,
            Message = message
        };

        return new ObjectResult(response)
        {
            StatusCode = statusCode
        };
    }

    // 创建错误响应的方法，用于常见的 HTTP 状态码。
    public static IActionResult CreateErrorResponse(HttpStatusCode statusCode, string message = null)
    {
        message ??= $"Error {(int)statusCode}";
        return FormatErrorResponse((int)statusCode, message);
    }
}
