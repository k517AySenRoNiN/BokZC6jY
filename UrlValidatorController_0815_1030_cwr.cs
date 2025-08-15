// 代码生成时间: 2025-08-15 10:30:10
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlValidatorApp.Controllers
{
    /// <summary>
    /// Controller for validating URL links.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class UrlValidatorController : ControllerBase
    {
        /// <summary>
        /// Validates the provided URL.
        /// </summary>
        /// <param name="url">The URL to be validated.</param>
        /// <returns>A JSON response indicating whether the URL is valid.</returns>
        [HttpGet]
        public async Task<ActionResult<bool>> ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("URL cannot be null or empty.");
            }

            try
            {
                Uri uriResult;
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result)
                {
                    using (var client = new WebClient())
                    {
                        await client.OpenReadAsync(uriResult); // This will throw an exception if the URL is not reachable.
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception details.
                // For simplicity, we're just returning false here, but you might want to handle different exceptions differently.
                return StatusCode(500, $"An error occurred while validating the URL: {ex.Message}");
            }
        }
    }
}