// 代码生成时间: 2025-08-08 06:13:51
 * Features:
 * 1. Addition
 * 2. Subtraction
 * 3. Multiplication
 * 4. Division
 *
 * Requirements:
 * 1. Code structure is clear and easy to understand.
 * 2. Proper error handling is included.
 * 3. Necessary comments and documentation are added.
 * 4. Adhere to C# best practices.
 * 5. Ensure code maintainability and scalability.
 */

using System;

namespace MathToolSet
{
    public class MathCalculationTool
    {
        // Adds two numbers.
        public double Add(double num1, double num2)
        {
            return num1 + num2;
        }

        // Subtracts the second number from the first.
        public double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }

        // Multiplies two numbers.
        public double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }

        // Divides the first number by the second.
        // Throws an exception if the denominator is zero.
        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return num1 / num2;
        }
    }

    // Application entry point.
    class Program
    {
        static void Main(string[] args)
        {
            MathCalculationTool calculator = new MathCalculationTool();

            try
            {
                double addResult = calculator.Add(10, 5);
                Console.WriteLine("Addition result: " + addResult);

                double subtractResult = calculator.Subtract(10, 5);
                Console.WriteLine("Subtraction result: " + subtractResult);

                double multiplyResult = calculator.Multiply(10, 5);
                Console.WriteLine("Multiplication result: " + multiplyResult);

                double divideResult = calculator.Divide(10, 5);
                Console.WriteLine("Division result: " + divideResult);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that may occur.
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}