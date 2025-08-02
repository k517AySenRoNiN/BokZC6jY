// 代码生成时间: 2025-08-03 04:36:29
using System;
using System.IO;
using System.Linq;

namespace FileBackupSync
{
    public class FileBackupSyncTool
    {
        private string sourceDirectory;
        private string backupDirectory;
        private string syncDirectory;

        // Constructor to initialize the directories
        public FileBackupSyncTool(string sourceDir, string backupDir, string syncDir)
        {
            sourceDirectory = sourceDir;
            backupDirectory = backupDir;
            syncDirectory = syncDir;
        }

        // Backup files from the source directory to the backup directory
        public void BackupFiles()
        {
            try
            {
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    var destFile = Path.Combine(backupDirectory, Path.GetFileName(file));
                    File.Copy(file, destFile, true); // Overwrite existing files
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during backup: {ex.Message}");
            }
        }

        // Synchronize files between source and sync directory
        public void SyncFiles()
        {
            try
            {
                // Create sync directory if it does not exist
                if (!Directory.Exists(syncDirectory))
                {
                    Directory.CreateDirectory(syncDirectory);
                }

                // Get all files in source and sync directories
                var sourceFiles = Directory.GetFiles(sourceDirectory).Select(Path.GetFileName).ToList();
                var syncFiles = Directory.GetFiles(syncDirectory).Select(Path.GetFileName).ToList();

                // Upload new files from source to sync directory
                foreach (var file in sourceFiles)
                {
                    if (!syncFiles.Contains(file))
                    {
                        var destFile = Path.Combine(syncDirectory, file);
                        File.Copy(Path.Combine(sourceDirectory, file), destFile, true);
                    }
                }

                // Delete files in sync directory that do not exist in the source directory
                foreach (var file in syncFiles)
                {
                    if (!sourceFiles.Contains(file))
                    {
                        File.Delete(Path.Combine(syncDirectory, file));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during synchronization: {ex.Message}");
            }
        }
    }
}
