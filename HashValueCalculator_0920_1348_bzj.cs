// 代码生成时间: 2025-09-20 13:48:09
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashValueCalculator
{
    /// <summary>
    /// Provides a tool for calculating hash values of strings.
    /// </summary>
    public class HashValueCalculator
    {
        /// <summary>
        /// Calculates the hash value of a given input string using the specified hash algorithm.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="hashAlgorithm">The name of the hash algorithm to use (e.g., "SHA256").</param>
        /// <returns>The hash value as a hexadecimal string.</returns>
        /// <exception cref="ArgumentException">Thrown when the hash algorithm is not supported.</exception>
        public string CalculateHash(string input, string hashAlgorithm)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            using (var hashAlgorithmInstance = HashAlgorithm.Create(hashAlgorithm))
            {
                if (hashAlgorithmInstance == null)
                {
                    throw new ArgumentException($"The hash algorithm '{hashAlgorithm}' is not supported.");
                }

                // Convert the input string to a byte array and compute the hash.
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = hashAlgorithmInstance.ComputeHash(inputBytes);

                // Convert the hash bytes to a hexadecimal string.
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
