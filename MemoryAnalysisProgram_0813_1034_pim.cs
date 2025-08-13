// 代码生成时间: 2025-08-13 10:34:50
using System;
using System.Diagnostics;
using System.Threading;

namespace MemoryAnalysis
{
    /// <summary>
    /// Program class to analyze memory usage.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Memory Analysis Program Started");
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                DisplayMemoryUsage(currentProcess);

                // Simulate memory usage by allocating a large byte array.
                byte[] largeArray = new byte[1024 * 1024 * 100]; // 100 MB
                DisplayMemoryUsage(currentProcess);

                // Give time to GC to collect objects.
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1000); // Wait for 1 second.

                // Display memory usage after garbage collection.
                DisplayMemoryUsage(currentProcess);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Displays the current memory usage of the process.
        /// </summary>
        /// <param name="process">The process whose memory usage is to be displayed.</param>
        private static void DisplayMemoryUsage(Process process)
        {
            Console.WriteLine("Physical Memory Usage: " +
                process.WorkingSet64 / (1024 * 1024) +
                " MB");
            Console.WriteLine("Virtual Memory Usage: " +
                process.VirtualMemorySize64 / (1024 * 1024) +
                " MB");
            Console.WriteLine("Private Memory Usage: " +
                process.PrivateMemorySize64 / (1024 * 1024) +
                " MB");
            Console.WriteLine();
        }
    }
}
