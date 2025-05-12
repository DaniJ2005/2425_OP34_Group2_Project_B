using System.Security.Cryptography;

public static class UserLogic
{
    public static User? CurrentUser { get; set; }

    public static User CheckEmail(string email)
    {
        User user = UserAccess.GetByEmail(email);
        if (user != null)
        {
            LoggerLogic.Instance.Log($"Login form | Correct email is enterd | {email}");

            return user;
        }
        LoggerLogic.Instance.Log($"Login form | Incorrect email is enterd |  {email}");

        return null;
    }
    public static User CheckLogin(string email, string password)
    {
        User user = UserAccess.GetByEmail(email);
        if (user != null && user.Password == password)
        {
            CurrentUser = user;
            LoggerLogic.Instance.Log($"User logged in | ID: {CurrentUser.Id} | Email: {CurrentUser.Email} | Role: {CurrentUser.RoleId}");

            return user;
        }
        LoggerLogic.Instance.Log($"User login failed | Email: {email}");

        return null;
    }

    public static void Logout()
    {
        if (CurrentUser != null)
        {
            LoggerLogic.Instance.Log($"User logged out | ID: {CurrentUser.Id} | Email: {CurrentUser.Email}");
            CurrentUser = null;
        }
    }

    public static User RegisterUser(string email, string password, string userName)
    {
        if (!ValidateEmail(email))
        {
            LoggerLogic.Instance.Log($"Registration failed | Invalid email: {email}");
            return null;
        }

        if (!ValidatePassword(password))
        {
            LoggerLogic.Instance.Log($"Registration failed | Password too short");
            return null;
        }

        if (!ValidateUserName(userName))
        {
            LoggerLogic.Instance.Log($"Registration failed | Invalid user name: {userName}");
            return null;
        }

        // Check if email already exists
        var existingUser = UserAccess.GetByEmail(email);
        if (existingUser != null)
        {
            LoggerLogic.Instance.Log($"Registration failed | Email already in use: {email}");
            return null;
        }

        var newUser = new User
        {
            Email = email,
            Password = password,
            UserName = userName
        };

        UserAccess.Write(newUser);
        LoggerLogic.Instance.Log($"User registered | Email: {email} | UserName: {userName}");

        CurrentUser = newUser;

        return newUser;
    }

    public static bool ValidateEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    public static bool ValidatePassword(string password)
    {
        return password.Length >= 8;
    }

    public static bool ValidateUserName(string userName)
    {
        return userName.Length >= 3;
    }

    // Move the Mask method here
    public static string Mask(string input) => new string('*', input.Length);

    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extract salt (first 16 bytes)
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Extract stored hash (next 32 bytes)
        byte[] storedPasswordHash = new byte[32];
        Array.Copy(hashBytes, 16, storedPasswordHash, 0, 32);

        // Hash the input password with the same salt
        var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] inputPasswordHash = pbkdf2.GetBytes(32);

        // Compare both hashes byte by byte
        return storedPasswordHash.SequenceEqual(inputPasswordHash);
    }
}
