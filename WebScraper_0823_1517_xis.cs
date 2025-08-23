// 代码生成时间: 2025-08-23 15:17:02
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using HtmlAgilityPack;

namespace WebScraperApplication
{
    /// <summary>
    /// A utility class for scraping web content.
    /// </summary>
    public class WebScraper
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScraper"/> class.
        /// </summary>
        public WebScraper()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Asynchronously fetches HTML content from the specified URL.
        /// </summary>
        /// <param name="url">The URL to scrape.</param>
        /// <returns>A string containing the HTML content.</returns>
        public async Task<string> FetchHtmlContentAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                // Log the exception or handle it as per application's error handling policy
                throw new ApplicationException("Failed to fetch HTML content.", e);
            }
        }

        /// <summary>
        /// Extracts and returns the title of the webpage from the HTML content.
        /// </summary>
        /// <param name="htmlContent">The HTML content of the webpage.</param>
        /// <returns>The title of the webpage.</returns>
        public string ExtractTitleFromHtml(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentException("HTML content cannot be null or whitespace.", nameof(htmlContent));

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");

            if (titleNode != null)
                return titleNode.InnerText;
            else
                throw new InvalidOperationException("Title tag not found in the HTML content.");
        }
    }
}
