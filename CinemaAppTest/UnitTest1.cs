namespace CinemaAppTest
{
    [TestClass]
    public class UnitTest1
    {
        private static AccountsLogic accountsLogic = new AccountsLogic();
        private static string Email = "danijurjevic2005@gmail.com";
        private static string Password = "test1234";

        [TestInitialize]
        public void Setup()
        {
            // Ensure the database file exists
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSources", "project.db");
            if (!File.Exists(dbPath))
            {
                Assert.Fail("Database file not found: " + dbPath);
            }

            // Ensure test account doesn't already exist
            var existingAccount = AccountsAccess.GetByEmail(Email);
            if (existingAccount != null)
            {
                AccountsAccess.Delete(existingAccount);
            }
        }

        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            AccountModel account = new(1, Email, Password, "Dani Jurjevic");
            AccountsAccess.Write(account);

            // Act
            var result = accountsLogic.CheckLogin(Email, Password);

            // Assert
            Assert.IsNotNull(result, "Login failed: Account should exist.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            var account = AccountsAccess.GetByEmail(Email);
            if (account != null)
            {
                AccountsAccess.Delete(account);
            }
        }
    }
}