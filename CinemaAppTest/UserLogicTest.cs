[TestClass]
public class UserLogicTest
{
    private User testUser;


    [TestInitialize]
    public void Setup()
    {
        testUser = new User
        {
            Email = "test@test",
            Password = "password123",
            UserName = "TestUser"
        };
    }

    [DataTestMethod]
    [DataRow("user@example.com", true)]
    [DataRow("user.name@domain.co", true)]
    [DataRow("user@domain", false)]
    [DataRow("userdomain.com", false)]
    [DataRow("@domain.com", true)]
    public void ValidateEmail_GivenVariousInputs_ReturnsExpected(string email, bool expected)
    {
        // Act
        var result = UserLogic.ValidateEmail(email);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("short", false)]
    [DataRow("1234567", false)]
    [DataRow("12345678", true)]
    [DataRow("123456789", true)]
    [DataRow("longpassword", true)]
    public void ValidatePassword_GivenVariousLengths_ReturnsExpected(string password, bool expected)
    {
        // Act
        var result = UserLogic.ValidatePassword(password);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("Al", false)]
    [DataRow("Ali", true)]
    [DataRow("Alice", true)]
    public void ValidateUserName_GivenVariousNames_ReturnsExpected(string username, bool expected)
    {
        // Act
        var result = UserLogic.ValidateUserName(username);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("secret", "******")]
    [DataRow("1234", "****")]
    [DataRow("", "")]
    public void Mask_GivenInputString_ReturnsMaskedString(string input, string expected)
    {
        // Act
        var result = UserLogic.Mask(input);

        // Assert
        Assert.AreEqual(expected, result);
    }
}
