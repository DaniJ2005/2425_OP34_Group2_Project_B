[TestClass]
public class UserLogicTest
{
    private User testUser;
    private User testAdminUser;

    [TestInitialize]
    public void Setup()
    {
        testUser = new User
        {
            Email = "test@test.com",
            Password = "hashedpassword",
            UserName = "Guest",
            RoleId = 0 // guest role
        };

        testAdminUser = new User
        {
            Email = "admint@test.com",
            Password = "VerySecurePassword",
            UserName = "Admin",
            RoleId = 1, // admin role
        };
    }

    [TestMethod]
    public void IsAdmin_WithAdminRole_ReturnsTrue()
    {
        // Arrange
        UserLogic.CurrentUser = testAdminUser;

        // Act
        var result = UserLogic.IsAdmin();

        // Assert
        Assert.IsTrue(result, $"Expected user to be admin but IsAdmin() returned {result}.");
    }

    [TestMethod]
    public void Logout_ClearsCurrentUser()
    {
        // Arrange
        UserLogic.CurrentUser = testUser;

        // Act
        UserLogic.Logout();

        // Assert
        Assert.IsNull(UserLogic.CurrentUser, "Expected CurrentUser to be null after logout.");
    }

    [DataTestMethod]
    [DataRow("user@example.com", true)] // Valid email with @ and .
    [DataRow("user.example.com", false)] // Missing @
    [DataRow("user@com", false)] // Missing dot after @
    [DataRow("", false)] // Empty email
    public void ValidateEmail_ReturnsExpected(string email, bool expected)
    {
        var result = UserLogic.ValidateEmail(email);
        Assert.AreEqual(expected, result, $"ValidateEmail failed for '{email}'");
    }

    [DataTestMethod]
    [DataRow("password", true)] // Valid password length 8
    [DataRow("pass", false)] // Too short password
    [DataRow("", false)] // Empty password
    [DataRow("        ", false)] // Long enough password but all whitespaces
    public void ValidatePassword_ReturnsExpected(string password, bool expected)
    {
        var result = UserLogic.ValidatePassword(password);
        Assert.AreEqual(expected, result, $"ValidatePassword failed for '{password}'");
    }

    [DataTestMethod]
    [DataRow("abc", true)] // Valid username length 3
    [DataRow("ab", false)] // Too short username length 2
    [DataRow("", false)] // Empty username
    [DataRow("   ", false)] // Long enough password but all whitespaces
    public void ValidateUserName_ReturnsExpected(string userName, bool expected)
    {
        var result = UserLogic.ValidateUserName(userName);
        Assert.AreEqual(expected, result, $"ValidateUserName failed for '{userName}'");
    }

    [TestMethod]
    public void Mask_Input_ReturnsAsterisksOfSameLength()
    {
        string input = "password123";
        string masked = UserLogic.Mask(input);
        Assert.AreEqual(input.Length, masked.Length);
        Assert.IsTrue(masked.All(c => c == '*'), "Mask should only contain asterisks.");
    }
}
