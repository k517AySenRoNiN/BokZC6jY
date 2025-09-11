// 代码生成时间: 2025-09-12 03:53:52
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkStatusCheck
{
    /// <summary>
    /// A class to check the network connection status.
    /// </summary>
    public class NetworkStatusChecker
    {
        /// <summary>
        /// Checks the network connectivity to the specified host.
        /// </summary>
        /// <param name="host">The host to check the connectivity with.</param>
        /// <param name="port">The port to check the connectivity on.</param>
        /// <returns>True if the network is connected, otherwise false.</returns>
        public async Task<bool> CheckNetworkConnectivityAsync(string host, int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(host, port);
                    return client.Connected;
                }
            }
            catch (SocketException socketException)
            {
                // Handle socket exceptions, such as network unreachable, etc.
                Console.WriteLine($"SocketException: {socketException.Message}");
                return false;
            }
            catch (PingException pingException)
            {
                // Handle ping exceptions, such as no response from host.
                Console.WriteLine($"PingException: {pingException.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Handle other exceptions.
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Checks the network connectivity to a default host and port.
        /// </summary>
        /// <returns>True if the network is connected, otherwise false.</returns>
        public async Task<bool> CheckNetworkConnectivityAsync()
        {
            // Use a default host and port, for example, Google's public DNS server.
            const string defaultHost = "8.8.8.8";
            const int defaultPort = 53;
            return await CheckNetworkConnectivityAsync(defaultHost, defaultPort);
        }
    }
}
