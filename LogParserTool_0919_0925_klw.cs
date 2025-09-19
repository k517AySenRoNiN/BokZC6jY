// 代码生成时间: 2025-09-19 09:25:37
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LogParserTool
{
    /// <summary>
    /// A class responsible for parsing log files.
    /// </summary>
    public class LogParser
    {
        private const string LogPattern = @"^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) - (.+) - (ERROR|WARNING|INFO|DEBUG)$";

        /// <summary>
        /// Parses a log file and returns a list of log entries.
        /// </summary>
        /// <param name="logFilePath">The path to the log file.</param>
        /// <returns>A list of parsed log entries.</returns>
        public List<LogEntry> ParseLogFile(string logFilePath)
        {
            if (string.IsNullOrEmpty(logFilePath))
            {
                throw new ArgumentException("Log file path cannot be null or empty.", nameof(logFilePath));
            }

            if (!File.Exists(logFilePath))
            {
                throw new FileNotFoundException("Log file not found.", logFilePath);
            }

            var logEntries = new List<LogEntry>();
            var regex = new Regex(LogPattern, RegexOptions.Compiled);

            using (var reader = new StreamReader(logFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        string timestamp = match.Groups[1].Value;
                        string message = match.Groups[2].Value;
                        string level = match.Groups[3].Value;
                        logEntries.Add(new LogEntry { Timestamp = timestamp, Message = message, Level = level });
                    }
                    else
                    {
                        // Handle lines that do not match the expected pattern if necessary
                    }
                }
            }

            return logEntries;
        }
    }

    /// <summary>
    /// Represents a log entry with timestamp, message, and level.
    /// </summary>
    public class LogEntry
    {
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
    }
}
