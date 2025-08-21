// 代码生成时间: 2025-08-22 03:14:06
// ErrorLogger.cs
// This class provides functionality for collecting and storing error logs in a file.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoggingExample
{
    // Define an enumeration for the error severity levels
    public enum ErrorSeverity
    {
        Info,
        Warning,
        Error,
        Fatal
    }

    public class ErrorLogger
    {
        // Path to the file where the errors will be logged
        private readonly string _logFilePath;

        // Constructor that initializes the logger with a file path
        public ErrorLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        // Method to log an error with its severity, message, and an optional exception
        public void LogError(ErrorSeverity severity, string message, Exception exception = null)
        {
            try
            {
                // Format the log message with severity, timestamp, and message
                string logEntry = $"[{DateTime.Now}][{severity}]: {message}";
                if (exception != null)
                {
                    logEntry += $" - Exception: {exception.Message}
Stack Trace: {exception.StackTrace}";
                }
                logEntry += Environment.NewLine;

                // Append the log entry to the log file
                File.AppendAllText(_logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during logging
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }

        // Asynchronous version of LogError for non-blocking operations
        public async Task LogErrorAsync(ErrorSeverity severity, string message, Exception exception = null)
        {
            try
            {
                string logEntry = $"[{DateTime.Now}][{severity}]: {message}";
                if (exception != null)
                {
                    logEntry += $" - Exception: {exception.Message}
Stack Trace: {exception.StackTrace}";
                }
                logEntry += Environment.NewLine;

                // Append the log entry to the log file asynchronously
                await File.AppendAllTextAsync(_logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
    }
}
