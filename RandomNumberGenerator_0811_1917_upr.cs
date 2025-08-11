// 代码生成时间: 2025-08-11 19:17:21
using System;
using System.Threading.Tasks;
# TODO: 优化性能

namespace RandomNumberApp
{
    /// <summary>
    /// Provides functionality to generate random numbers.
# TODO: 优化性能
    /// This class is designed to be thread-safe and easy to maintain.
    /// </summary>
    public class RandomNumberGenerator
    {
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the RandomNumberGenerator class.
        /// </summary>
        public RandomNumberGenerator()
        {
            _random = new Random();
# 改进用户体验
        }

        /// <summary>
        /// Generates a random integer number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the range.</param>
# TODO: 优化性能
        /// <param name="maxValue">The exclusive upper bound of the range.</param>
        /// <returns>A random integer number between minValue and maxValue.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if minValue is greater than maxValue.</exception>
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
# NOTE: 重要实现细节
                throw new ArgumentOutOfRangeException(nameof(minValue), "MinValue cannot be greater than MaxValue.");
            }

            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generates a random double number between 0 and 1.
        /// </summary>
        /// <returns>A random double number between 0 and 1.</returns>
        public double GenerateRandomDouble()
        {
            return _random.NextDouble();
# FIXME: 处理边界情况
        }
    }
}
# TODO: 优化性能
