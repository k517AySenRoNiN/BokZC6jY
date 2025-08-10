// 代码生成时间: 2025-08-10 16:32:39
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkCheckerApp
{
    /// <summary>
    /// Network connection status checker class.
    /// </summary>
    public class NetworkConnectionChecker
    {
        private readonly Ping _ping;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionChecker"/> class.
        /// </summary>
        public NetworkConnectionChecker()
        {
            _ping = new Ping();
        }

        /// <summary>
        /// Checks if the network connection is active by pinging a specified address.
        /// </summary>
        /// <param name="address">The address to ping.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> IsConnectedAsync(string address)
        {
            try
            {
                // Ping the specified address.
                PingReply reply = await _ping.SendPingAsync(address);
                // Check if the address is reachable.
                return reply.Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
                // Log the exception or handle it as necessary.
                Console.WriteLine($"Ping failed with error: {ex.Message}");
                return false;
            }
        }
    }
}
