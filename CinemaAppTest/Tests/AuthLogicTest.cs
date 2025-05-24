namespace CinemaAppTest.Tests
{
    /// <summary>
    /// Unit tests for the UserLogic validation methods.
    /// 
    /// Validation Rules:
    /// - Email must contain "@" and ".".
    /// - Password must be at least 8 characters long.
    /// - Username must be at least 3 characters long.
    /// </summary>

    [TestClass]
    public class UserLogicTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Reset static state
            UserLogic.CurrentUser = null;
        }

        /// <summary>
        /// Tests email validation.
        /// Validation Rule: Email must contain "@" and ".".
        /// </summary>
        [DataTestMethod]
        [DataRow("user@example.com", true)]
        [DataRow("user@domain.co.uk", true)]
        [DataRow("user", false)]
        [DataRow("user@", false)]
        [DataRow("@example.com", true)]
        [DataRow("userexample.com", false)]
        public void ValidateEmail_ReturnsExpectedResult(string email, bool expected)
        {
            // Act
            bool result = UserLogic.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expected, result, $"Email validation failed for input '{email}'. Expected: {expected}, Actual: {result}");
        }

        /// <summary>
        /// Tests password validation.
        /// Validation Rule: Password must be at least 8 characters long.
        /// </summary>
        [DataTestMethod]
        [DataRow("password123", true)]     // 11 chars
        [DataRow("12345678", true)]        // 8 chars
        [DataRow("short", false)]          // 5 chars
        [DataRow("", false)]               // 0 chars
        [DataRow("1234567", false)]        // 7 chars
        public void ValidatePassword_ReturnsExpectedResult(string password, bool expected)
        {
            // Act
            bool result = UserLogic.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, result, $"Password validation failed for input '{password}'. Expected: {expected}, Actual: {result}");
        }

        /// <summary>
        /// Tests username validation.
        /// Validation Rule: Username must be at least 3 characters long.
        /// </summary>
        [DataTestMethod]
        [DataRow("John", true)]    // 4 chars
        [DataRow("JD", false)]     // 2 chars
        [DataRow("", false)]       // 0 chars
        [DataRow("Abc", true)]     // 3 chars
        public void ValidateUserName_ReturnsExpectedResult(string userName, bool expected)
        {
            // Act
            bool result = UserLogic.ValidateUserName(userName);

            // Assert
            Assert.AreEqual(expected, result, $"Username validation failed for input '{userName}'. Expected: {expected}, Actual: {result}");
        }

        /// <summary>
        /// Tests masking of input strings.
        /// Example: "secret" becomes "******".
        /// </summary>
        [DataTestMethod]
        [DataRow("secret", "******")]         // 6 chars
        [DataRow("abc", "***")]               // 3 chars
        [DataRow("", "")]                     // 0 chars
        [DataRow("123456789", "*********")]   // 9 chars
        public void Mask_ReturnsMaskedString(string input, string expected)
        {
            // Act
            string result = UserLogic.Mask(input);

            // Assert
            Assert.AreEqual(expected, result, $"Masking failed for input '{input}'. Expected: '{expected}', Actual: '{result}'");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Cleanup static state
            UserLogic.CurrentUser = null;
        }
    }
}
