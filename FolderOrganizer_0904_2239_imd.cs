// 代码生成时间: 2025-09-04 22:39:48
using System;
using System.IO;
using System.Linq;

namespace FolderOrganizerApp
{
    /// <summary>
    /// This class is responsible for organizing the folder structure.
    /// </summary>
    public class FolderOrganizer
    {
        /// <summary>
        /// The main method to organize the folder structure.
        /// </summary>
        /// <param name="sourcePath">The path of the source folder to organize.</param>
        /// <returns>True if the organization is successful; otherwise, false.</returns>
        public bool OrganizeFolders(string sourcePath)
        {
            // Check if the provided path is valid
            if (!Directory.Exists(sourcePath))
            {
                Console.WriteLine("The source path does not exist.");
                return false;
            }

            try
            {
                // Retrieve all files from the source path
                var files = Directory.GetFiles(sourcePath);

                // Create a new directory for organized files if it does not exist
                string organizedDirectory = Path.Combine(sourcePath, "Organized");
                if (!Directory.Exists(organizedDirectory))
                {
                    Directory.CreateDirectory(organizedDirectory);
                }

                // Move files to the organized directory
                foreach (var file in files)
                {
                    // Extract the file extension to determine the new path
                    string extension = Path.GetExtension(file);
                    string newDirectory = Path.Combine(organizedDirectory, extension.TrimStart('.'));

                    // Create the new directory if it does not exist
                    if (!Directory.Exists(newDirectory))
                    {
                        Directory.CreateDirectory(newDirectory);
                    }

                    // Move the file to the new directory
                    string newFilePath = Path.Combine(newDirectory, Path.GetFileName(file));
                    File.Move(file, newFilePath);
                }

                Console.WriteLine("Folder organization is complete.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            // Create an instance of the FolderOrganizer class
            FolderOrganizer organizer = new FolderOrganizer();

            // Check if a source path is provided as a command line argument
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide a source path as an argument.");
                return;
            }
            string sourcePath = args[0];

            // Attempt to organize the folders
            bool success = organizer.OrganizeFolders(sourcePath);

            // Provide feedback to the user
            Console.WriteLine(success ? "Folders organized successfully." : "Folder organization failed.");
        }
    }
}