// 代码生成时间: 2025-08-24 08:03:49
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace WebContentScraper
{
    /// <summary>
    /// A web content scraping tool to fetch and extract content from web pages.
    /// </summary>
    public class WebContentScraper
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebContentScraper"/> class.
        /// </summary>
        public WebContentScraper()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Scrapes the content from the specified URL.
        /// </summary>
        /// <param name="url">The URL to scrape content from.</param>
        /// <returns>The scraped content as a string.</returns>
        public async Task<string> ScrapeContentAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }

            try
            {
                // Send a GET request to the specified URL
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();
                return ExtractContent(content);
            }
            catch (HttpRequestException e)
            {
                // Handle any exceptions that occur during the HTTP request
                throw new Exception("An error occurred while scraping content.", e);
            }
        }

        /// <summary>
        /// Extracts the main content from the HTML string using regex to remove scripts and styles.
        /// </summary>
        /// <param name="html">The HTML content to extract from.</param>
        /// <returns>The extracted content.</returns>
        private string ExtractContent(string html)
        {
            // Remove script and style elements from the HTML
            string pattern = "<head>.*?<\/head>|<script.*?<\/script>|" +
                            