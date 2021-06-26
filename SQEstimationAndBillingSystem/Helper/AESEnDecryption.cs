using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace SquarishSQEstimationAndBillingSystem.Helper
{
    /// <summary>
    /// Encryption and Decryption logic
    /// </summary>
    public class AESEnDecryption
    {
        #region Encryption Keys
        private static String secretKey = "bNEYAtvsawjPjIjqoCaYWgcbaF773C7V";
        private static String IVKey = "HR$2pIjHR$2pIj12";
        private static String UniqueIDKey = "tqYDyvFmhjpaE43m9HkeptyuEnVmb24w";
        #endregion

        /// <summary>
        /// Encyption Logic Intitalization 
        /// </summary>
        /// <returns></returns>
        public static RijndaelManaged GetRijndaelManaged()
        {            
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var IVBytes = Encoding.UTF8.GetBytes(IVKey);
           
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = IVBytes
            };
        }

        /// <summary>
        /// Method Encrypt plain text
        /// </summary>
        /// <param name="plainBytes">Plain text</param>
        /// <param name="rijndaelManaged">Encyption keys object</param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        /// <summary>
        /// Method to Decrypt encrypted text
        /// </summary>
        /// <param name="encryptedData">Data to be decrypted</param>
        /// <param name="rijndaelManaged">Encyption keys object</param>
        /// <returns></returns>
        private static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        /// <summary>
        /// Encrypts plaintext using AES 128bit key and a Chain Block Cipher and returns a base64 encoded string
        /// </summary>
        /// <param name="plainText">Plain text to encrypt</param>
        /// <param name="key">Secret key</param>
        /// <returns>Base64 encoded string</returns>
        public static String Encrypt(String plainText)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged()));
        }

        /// <summary>
        /// Decrypts a base64 encoded string using the given key (AES 128bit key and a Chain Block Cipher)
        /// </summary>
        /// <param name="encryptedText">Base64 Encoded String</param>
        /// <param name="key">Secret Key</param>
        /// <returns>Decrypted String</returns>
        public static String Decrypt(String encryptedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged()));
        }

        /// <summary>
        /// Method to create Authorization Token
        /// </summary>
        /// <param name="ticks">Current time tick to be used as a seed to create Auth Token</param>
        /// <returns></returns>
        public static string CreateToken(long ticks)
        {
            string hashLeft = string.Empty;
            string hashRight = string.Empty;
            string encry1 = string.Empty;
            string encry2 = string.Empty;

            try
            {
                string key = Convert.ToString(secretKey);
                string IV = Convert.ToString(IVKey);
                string UniqueID = Convert.ToString(UniqueIDKey);

              
                
                string tokn = string.Join(":", new string[] { UniqueID, Encrypt(ticks.ToString()) });              

                var basestring = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokn));

                return basestring;

            }
            catch 
            {
                throw;
            }
        }
    }
}