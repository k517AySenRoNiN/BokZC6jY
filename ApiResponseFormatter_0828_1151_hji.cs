// 代码生成时间: 2025-08-28 11:51:44
 * This class is designed to format API responses into a consistent structure.
 * It includes error handling and follows C# best practices for maintainability and scalability.
 */
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiFormatter
{
    /// <summary>
    /// Provides functionality to format API responses.
    /// </summary>
    public class ApiResponseFormatter
    {
        /// <summary>
        /// Formats a successful API response with a given data object.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <param name="data">The data object to include in the response.</param>
        /// <returns>A formatted API response.</returns>
        public IActionResult FormatSuccessResponse<T>(T data)
        {
            var response = new
            {
                success = true,
                data = data,
                message = "Request processed successfully."
            };

            return new OkObjectResult(response);
        }

        /// <summary>
        /// Formats an API response for an error with a specified error message and status code.
        /// </summary>
        /// <param name="message">The error message to include in the response.</param>
        /// <param name="statusCode">The HTTP status code for the error.</param>
        /// <returns>A formatted API error response.</returns>
        public IActionResult FormatErrorResponse(string message, HttpStatusCode statusCode)
        {
            var response = new
            {
                success = false,
                message = message
            };

            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }

        // Additional methods for different types of responses can be added here.
    }
}
