// 代码生成时间: 2025-08-27 13:22:51
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryptionDecryptionTool
{
    /// <summary>
    /// 密码加密解密工具类
    /// </summary>
    public class PasswordEncryptionDecryptionTool
    {
        private const string EncryptionKey = "your_encryption_key"; // 加密密钥
        private const string Salt = "your_salt"; // 加盐值

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">待加密的密码</param>
        /// <returns>加密后的密码</returns>
        public string EncryptPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("密码不能为空", nameof(password));
            }

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(Salt);

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }

                    var decryptedContent = msEncrypt.ToArray();
                    return Convert.ToBase64String(decryptedContent);
                }
            }
        }

        /// <summary>
        /// 解密密码
        /// </summary>
        /// <param name="encryptedPassword">待解密的加密密码</param>
        /// <returns>解密后的密码</returns>
        public string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrWhiteSpace(encryptedPassword))
            {
                throw new ArgumentException("加密密码不能为空", nameof(encryptedPassword));
            }

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(Salt);

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var swDecrypt = new StreamReader(csDecrypt))
                        {
                            return swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
