namespace CinemaAppTest.Tests
{
    /// <summary>
    /// Unit tests for the CryptoHelper class.
    /// 
    /// Validation Rules:
    /// - Hash should return a hashed string.
    /// - Verify should return true if the hash of input matches the stored hash.
    /// - VerifyEncrypted should compare raw input string to encrypted string directly.
    /// </summary>
    [TestClass]
    public class CryptoHelperTests
    {
        /// <summary>
        /// Tests that hashing the same input produces the same hash (deterministic).
        /// </summary>
        [TestMethod]
        public void Hash_SameInput_ProducesSameHash()
        {
            // Arrange
            string password = "MySecurePassword";

            // Act
            string hash1 = CryptoHelper.Hash(password);
            string hash2 = CryptoHelper.Hash(password);

            // Assert
            Assert.AreEqual(hash1, hash2, $"Hash values should match for the same input. Hash1: {hash1}, Hash2: {hash2}");
        }

        /// <summary>
        /// Tests that different inputs produce different hashes.
        /// </summary>
        [TestMethod]
        public void Hash_DifferentInputs_ProduceDifferentHashes()
        {
            // Arrange
            string input1 = "PasswordOne";
            string input2 = "PasswordTwo";

            // Act
            string hash1 = CryptoHelper.Hash(input1);
            string hash2 = CryptoHelper.Hash(input2);

            // Assert
            Assert.AreNotEqual(hash1, hash2, $"Hash values should differ for different inputs. Hash1: {hash1}, Hash2: {hash2}");
        }

        /// <summary>
        /// Tests the Verify method for correct and incorrect passwords.
        /// </summary>
        [TestMethod]
        public void Verify_InputMatchesStoredHash_ReturnsTrue()
        {
            // Arrange
            string password = "TestPassword123";
            string storedHash = CryptoHelper.Hash(password);

            // Act
            bool result = CryptoHelper.Verify(password, storedHash);

            // Assert
            Assert.IsTrue(result, $"Verify should return true for matching password and hash. Input: {password}, Hash: {storedHash}");
        }

        [TestMethod]
        public void Verify_InputDoesNotMatchStoredHash_ReturnsFalse()
        {
            // Arrange
            string correctPassword = "CorrectPassword";
            string wrongPassword = "WrongPassword";
            string storedHash = CryptoHelper.Hash(correctPassword);

            // Act
            bool result = CryptoHelper.Verify(wrongPassword, storedHash);

            // Assert
            Assert.IsFalse(result, $"Verify should return false for mismatched input. Input: {wrongPassword}, Stored Hash: {storedHash}");
        }

        /// <summary>
        /// Tests the VerifyEncrypted method which compares raw encrypted input strings.
        /// </summary>
        [TestMethod]
        public void VerifyEncrypted_MatchingInputs_ReturnsTrue()
        {
            // Arrange
            string encrypted = "1e2n3c4r5y6p7t8e9d";

            // Act
            bool result = CryptoHelper.VerifyEncrypted("1e2n3c4r5y6p7t8e9d", encrypted);

            // Assert
            Assert.IsTrue(result, $"VerifyEncrypted should return true for matching strings. Input: 'abc123encrypted'");
        }

        [TestMethod]
        public void VerifyEncrypted_NonMatchingInputs_ReturnsFalse()
        {
            // Arrange
            string encrypted = "1e2n3c4r5y6p7t8e9d";

            // Act
            bool result = CryptoHelper.VerifyEncrypted("differentEncrypted", encrypted);

            // Assert
            Assert.IsFalse(result, $"VerifyEncrypted should return false for mismatched strings. Input: 'differentEncrypted', Stored: '{encrypted}'");
        }
    }
}
