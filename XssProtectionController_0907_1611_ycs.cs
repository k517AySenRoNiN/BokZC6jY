// 代码生成时间: 2025-09-07 16:11:57
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace XssProtectionDemo
{
    /// <summary>
    /// Provides an endpoint to demonstrate XSS protection.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]"])
    public class XssProtectionController : ControllerBase
    {
        private readonly TextEncoder _encoder;

        /// <summary>
        /// Initializes a new instance of <see cref="XssProtectionController"/>.
        /// </summary>
        /// <param name="encoder">The text encoder for HTML encoding.</param>
        public XssProtectionController(TextEncoder encoder)
        {
            _encoder = encoder;
        }

        /// <summary>
        /// Handles GET requests to demonstrate XSS protection.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        /// <summary>
        /// Handles POST requests with user input to demonstrate XSS protection.
        /// </summary>
        /// <param name="input">The user input to be sanitized.</param>
        /// <returns>A view with the sanitized user input.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] string input)
        {
            try
            {
                // Sanitize the input to prevent XSS attacks.
                var sanitizedInput = _encoder.Encode(input);
                ViewBag.Message = sanitizedInput;
                return View("Get");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the sanitization process.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
