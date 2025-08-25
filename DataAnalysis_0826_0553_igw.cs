// 代码生成时间: 2025-08-26 05:53:09
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysisApp
{
    /// <summary>
    /// A class that provides data analysis functionality.
    /// </summary>
    public class DataAnalysis
    {
        /// <summary>
        /// Analyzes the given data set and returns statistical information.
        /// </summary>
        /// <param name="data">The data set to be analyzed.</param>
        /// <returns>A dictionary containing statistical information.</returns>
        public Dictionary<string, double> AnalyzeData(List<double> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");
            }

            if (!data.Any())
            {
                throw new ArgumentException("Data set is empty.", nameof(data));
            }

            var results = new Dictionary<string, double>();

            // Calculate mean
            double mean = data.Average();
            results.Add("Mean", mean);

            // Calculate median
            double median = data.OrderBy(x => x).Skip((data.Count - 1) / 2).Take(1).FirstOrDefault();
            if (data.Count % 2 == 0)
            {
                median = (data.OrderBy(x => x).ElementAt((data.Count / 2) - 1) + data.OrderBy(x => x).ElementAt(data.Count / 2)) / 2;
            }
            results.Add("Median", median);

            // Calculate mode
            int modeCount = 0;
            double mode = 0;
            foreach (var number in data)
            {
                var count = data.Count(n => n == number);
                if (count > modeCount)
                {
                    mode = number;
                    modeCount = count;
                }
            }
            results.Add("Mode", mode);

            // Calculate standard deviation
            double variance = data.Average(n => Math.Pow(n - mean, 2));
            double standardDeviation = Math.Sqrt(variance);
            results.Add("StandardDeviation", standardDeviation);

            return results;
        }
    }
}
