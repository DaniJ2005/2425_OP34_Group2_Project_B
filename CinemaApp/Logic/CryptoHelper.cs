using System;
using System.Security.Cryptography;
using System.Text;

public static class CryptoHelper
{
    // Static key from environment or fallback
    private static readonly string StaticKey = Environment.GetEnvironmentVariable("STATIC_KEY") ?? "fallback-secret";  // You can set this from environment if needed

    // Fixed salt (16 bytes) for deterministic hashing
    private static readonly byte[] FixedSalt = Encoding.UTF8.GetBytes("fixed-salt-123456");

    // Hash the password+static key deterministically with a fixed salt
    public static string Hash(string input)
    {
        string combined = input + StaticKey;

        // Use PBKDF2 with a high iteration count for security (adjust as necessary)
        using var pbkdf2 = new Rfc2898DeriveBytes(combined, FixedSalt, 500_000, HashAlgorithmName.SHA256); // 500,000 iterations for better security
        byte[] hash = pbkdf2.GetBytes(32); // 32-byte hash length

        return Convert.ToBase64String(hash);
    }

    // Verify the input password against the stored hash
    public static bool Verify(string input, string storedHash) => Hash(input) == storedHash;
    public static bool VerifyEncrypted(string input, string storedHash) => input == storedHash;
}
