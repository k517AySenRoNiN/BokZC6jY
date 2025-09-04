// 代码生成时间: 2025-09-04 17:54:20
// FileDecompressionTool.cs
// This is a utility class to handle file decompression in C# using ASP.NET framework.

using System;
using System.IO;
using System.IO.Compression;

namespace FileDecompressionTool
{
    public class FileDecompressionUtility
    {
        // Decompress a zip file to a specified directory
        public static void DecompressZipFile(string zipFilePath, string outputDirectory)
        {
            // Check if the path to the zip file is valid
            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException("The specified zip file was not found.", zipFilePath);
            }

            // Ensure the output directory exists, or create it if it doesn't
            Directory.CreateDirectory(outputDirectory);

            // Decompress the zip file to the specified directory
            try
            {
                ZipFile.ExtractToDirectory(zipFilePath, outputDirectory);
                Console.WriteLine($"Decompression completed successfully. Files extracted to: {outputDirectory}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during decompression
                Console.WriteLine($"An error occurred during decompression: {ex.Message}");
                throw;
            }
        }

        // Main method for demonstration purposes; in a real application, this would be an API endpoint
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: FileDecompressionTool <zipFilePath> <outputDirectory>");
                return;
            }

            string zipFilePath = args[0];
            string outputDirectory = args[1];

            try
            {
                DecompressZipFile(zipFilePath, outputDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}