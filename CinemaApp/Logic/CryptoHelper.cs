using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CryptoHelper
{
    private static readonly string key = "your-encryption-key-here"; // 16-byte key for AES-128
    private static readonly string iv = "your-encryption-iv-here";   // 16-byte IV (Initialization Vector) for AES

    // Encrypts the input string using AES encryption
    public static string Encrypt(string input)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key); // Convert the key to byte array
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);   // Convert the IV to byte array

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(input);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray()); // Return the encrypted string as base64
            }
        }
    }

    // Decrypts an encrypted string back to its original form
    public static string Decrypt(string encryptedInput)
    {
        byte[] cipherText = Convert.FromBase64String(encryptedInput);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    // Compares the plaintext password to the stored encrypted password
    public static bool Verify(string password, string storedEncryptedPassword)
    {
        // Encrypt the plaintext password entered by the user
        string encryptedPassword = Encrypt(password);

        // Compare the encrypted password with the stored encrypted password
        return encryptedPassword == storedEncryptedPassword; 
    }

    // Compares an encrypted password to a stored encrypted password directly
    public static bool VerifyEncryptedPassword(string encryptedPassword, string storedEncryptedPassword) => encryptedPassword == storedEncryptedPassword;
}
