// 代码生成时间: 2025-09-10 13:44:59
 * A simple random number generator class that can be used to generate random integer numbers.
 * This class demonstrates basic principles of C# programming, including error handling,
 * documentation, and following best practices.
 */

using System;

namespace RandomNumberGeneratorApp
{
    /// <summary>
    /// Class responsible for generating random numbers.
    /// </summary>
    public class RandomNumberGenerator
    {
        private Random _random;

        /// <summary>
        /// Initializes a new instance of the RandomNumberGenerator class.
        /// </summary>
        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Generates a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the range.</param>
        /// <param name="maxValue">The exclusive upper bound of the range.</param>
        /// <returns>The generated random integer.</returns>
        /// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue.</exception>
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            // Validate input parameters to ensure minValue is less than or equal to maxValue
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue cannot be greater than maxValue.");
            }

            // Generate and return a random number within the specified range
            return _random.Next(minValue, maxValue);
        }
    }
}
