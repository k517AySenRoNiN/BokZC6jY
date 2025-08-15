// 代码生成时间: 2025-08-15 17:37:40
 * Author: [Your Name]
 * Date: [Today's Date]
 */

using System;

namespace MathUtilities
{
    // MathTools class providing basic mathematical operations.
    public static class MathTools
    {
        // Adds two numbers.
        public static double Add(double a, double b)
        {
            try
            {
                return a + b;
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately.
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Subtracts one number from another.
        public static double Subtract(double a, double b)
        {
            try
            {
                return a - b;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Multiplies two numbers.
        public static double Multiply(double a, double b)
        {
            try
            {
                return a * b;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Divides one number by another.
        public static double Divide(double a, double b)
        {
            try
            {
                if (b == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero.");
                }
                return a / b;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Calculates the power of a number.
        public static double Power(double a, double b)
        {
            try
            {
                return Math.Pow(a, b);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}