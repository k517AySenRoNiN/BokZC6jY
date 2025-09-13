// 代码生成时间: 2025-09-13 23:38:06
using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkStatusChecker
{
    /// <summary>
    /// A class to check network connection status.
    /// </summary>
    public class NetworkStatusChecker
    {
        private readonly HttpClient _httpClient;
        private readonly string _testUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatusChecker"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance.</param>
        /// <param name="testUrl">The URL to test the connection.</param>
        public NetworkStatusChecker(HttpClient httpClient, string testUrl = "https://www.google.com")
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _testUrl = testUrl;
        }

        /// <summary>
        /// Checks if the network is connected by sending a request to a test URL.
        /// </summary>
        /// <returns>A boolean indicating the network connection status.</returns>
        public async Task<bool> IsConnectedAsync()
        {
            try
            {
                // Send a request to the test URL
                var response = await _httpClient.GetAsync(_testUrl);
                response.EnsureSuccessStatusCode();
                return true; // If the request is successful, the network is connected
            }
            catch (HttpRequestException)
            {
                // Handle any exceptions related to network connectivity
                return false;
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks if the network is connected by using the Ping method.
        /// </summary>
        /// <returns>A boolean indicating the network connection status.</returns>
        public async Task<bool> IsConnectedViaPingAsync()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(_testUrl);
                    return reply.Status == IPStatus.Success; // If the ping is successful, the network is connected
                }
            }
            catch (PingException)
            {
                // Handle any exceptions related to network connectivity
                return false;
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
