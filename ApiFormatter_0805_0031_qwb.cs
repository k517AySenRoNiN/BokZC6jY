// 代码生成时间: 2025-08-05 00:31:43
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiFormatter
{
    public class ApiFormatter
    {
        // 格式化API响应
        public IActionResult FormatApiResponse<T>(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            try
            {
                // 创建一个统一的响应对象
                var response = new
                {
                    Success = true,
                    StatusCode = (int)statusCode,
                    Data = data,
                    Message = statusCode == HttpStatusCode.OK ? "Operation successful." : "An error occurred."
                };

                // 返回JSON格式的响应
                return new ObjectResult(response) { StatusCode = (int)statusCode };
            }
            catch (Exception ex)
            {
                // 记录日志
                // Logger.LogError(ex, "An error occurred while formatting API response.");

                // 返回错误响应
                var errorResponse = new
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An internal server error occurred."
                };

                return new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}
