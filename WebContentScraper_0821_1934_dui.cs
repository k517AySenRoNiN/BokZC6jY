// 代码生成时间: 2025-08-21 19:34:44
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace WebContentScraper
{
    public class WebContentScraperService
    {
        private readonly ILogger<WebContentScraperService> _logger;
        private readonly HttpClient _httpClient;

        public WebContentScraperService(ILogger<WebContentScraperService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Fetches and returns the content of the specified URL.
        /// </summary>
        /// <param name="url">The URL to scrape.</param>
        /// <param name="userAgent">The user agent string to use for the request.</param>
        /// <returns>The HTML content of the URL.</returns>
        public async Task<string> ScrapeContentAsync(string url, string userAgent)
        {
            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("URL cannot be null or empty.");
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {
                // Set the user agent for the request
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                // Send a GET request to the URL
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                // Return the content of the response as a string
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while scraping content from {url}.", url);
                throw;
            }
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var url = "http://example.com"; // Replace with the URL you want to scrape
            var userAgent = "Mozilla/5.0 (compatible; YourBot/1.0; +http://yourdomain.com/bot.html)"; // Replace with your bot's user agent

            // Create an instance of the service and start scraping
            var scraperService = new WebContentScraperService(
                new LoggerFactory().CreateLogger<WebContentScraperService>(),
                new HttpClient()
            );

            try
            {
                var content = await scraperService.ScrapeContentAsync(url, userAgent);
                Console.WriteLine("Scraped Content: ");
                Console.WriteLine(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
