// 代码生成时间: 2025-08-02 12:45:14
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryptionDecryptionTool
{
    /// <summary>
    /// A utility class for encrypting and decrypting passwords using AES.
    /// </summary>
    public class PasswordEncryptionDecryptionTool
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;
        private readonly SymmetricAlgorithm _algorithm;

        /// <summary>
        /// Initializes a new instance of the PasswordEncryptionDecryptionTool class.
        /// </summary>
        public PasswordEncryptionDecryptionTool()
        {
            // Define the key and IV for AES encryption.
            // These should be securely stored and managed in a production environment.
            _key = Encoding.UTF8.GetBytes("your-encryption-key-here");
            _iv = Encoding.UTF8.GetBytes("your-initialization-vector-here");

            // Create an AES algorithm instance.
            _algorithm = Aes.Create();
            _algorithm.Key = _key;
            _algorithm.IV = _iv;
        }

        /// <summary>
        /// Encrypts a password.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password.</returns>
        public string EncryptPassword(string password)
        {
            try
            {
                using (var encryptor = _algorithm.CreateEncryptor())
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(password);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Encryption failed.", ex);
            }
        }

        /// <summary>
        /// Decrypts a password.
        /// </summary>
        /// <param name="encryptedPassword">The encrypted password to decrypt.</param>
        /// <returns>The decrypted password.</returns>
        public string DecryptPassword(string encryptedPassword)
        {
            try
            {
                using (var decryptor = _algorithm.CreateDecryptor())
                {
                    using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(encryptedPassword)))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Decryption failed.", ex);
            }
        }
    }
}
