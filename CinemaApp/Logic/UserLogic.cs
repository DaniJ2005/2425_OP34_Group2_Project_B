public static class UserLogic
{
    public static User? CurrentUser { get; set; }
    public static bool CanManageFoodMenu { get; private set; }
    public static bool CanManageAccounts { get; private set; }
    public static bool CanManageGuestAccounts { get; private set; }
    public static bool CanManageMovieSessions { get; private set; }
    public static bool CanManageMovieHall { get; private set; }
    public static bool CanManageReservations { get; private set; }

    public static bool IsAuthenticated() => SessionDataLogic.IsAuthenticated();

    public static bool IsAdmin() => CurrentUser != null && CurrentUser.RoleId != 0;

    public static User CheckEmail(string email)
    {
        var user = UserAccess.GetByEmail(email);
        LoggerLogic.Instance.Log(user != null
            ? $"Login form | Correct email entered | {email}"
            : $"Login form | Incorrect email entered | {email}");
        return user!;
    }

    public static User Login(string email, string password, bool passwordIsEncrypted = false)
    {
        var user = UserAccess.GetByEmail(email);

        if (user != null)
        {
            if (passwordIsEncrypted)
                if (CryptoHelper.VerifyEncrypted(password, user.Password))
                    return LoginUser(user);

            if (CryptoHelper.Verify(password, user.Password))
                return LoginUser(user);
        }

        LoggerLogic.Instance.Log($"User login failed | Email: {email}");
        return null!;
    }

    private static User LoginUser(User user)
    {
        CurrentUser = user;
        LoggerLogic.Instance.Log($"User logged in | ID: {user.Id} | Email: {user.Email} | Role: {user.RoleId}");
        SetPermissions();
        SessionDataLogic.MarkAuthenticated(CurrentUser);
        return user;
    }

    public static void Logout()
    {
        if (CurrentUser != null)
        {
            LoggerLogic.Instance.Log($"User logged out | ID: {CurrentUser.Id} | Email: {CurrentUser.Email}");
            CurrentUser = null;
            SessionDataLogic.Logout();
        }
    }

    public static User RegisterUser(string email, string password, string userName)
    {
        if (!ValidateEmail(email))
        {
            LoggerLogic.Instance.Log($"Registration failed | Invalid email: {email}");
            return null!;
        }

        if (!ValidatePassword(password))
        {
            LoggerLogic.Instance.Log($"Registration failed | Password too short");
            return null!;
        }

        if (!ValidateUserName(userName))
        {
            LoggerLogic.Instance.Log($"Registration failed | Invalid user name: {userName}");
            return null!;
        }

        var existingUser = UserAccess.GetByEmail(email);
        if (existingUser != null)
        {
            LoggerLogic.Instance.Log($"Registration failed | Email already in use: {email}");
            return null!;
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
        SessionDataLogic.MarkAuthenticated(newUser);

        return newUser;
    }

    public static bool ValidateEmail(string email) => email.Contains("@") && email.Contains(".");

    public static bool ValidatePassword(string password) => password.Length >= 8;

    public static bool ValidateUserName(string userName) => userName.Length >= 3;

    public static string Mask(string input) => new string('*', input.Length);

    private static void SetPermissions()
    {
        if (CurrentUser?.RoleId != null)
        {
            var role = RoleAccess.GetRoleById(CurrentUser.RoleId);

            if (role != null) //not a normal user
            {
                CanManageFoodMenu = role.ManageFoodMenu;
                CanManageAccounts = role.ManageAccounts;
                CanManageGuestAccounts = role.ManageGuestAccounts;
                CanManageMovieSessions = role.ManageMovieSessions;
                CanManageMovieHall = role.ManageMovieHall;
                CanManageReservations = role.ManageReservations;
            }
        }
    }

    public static string GetRole()
    {
        if (CurrentUser?.RoleId == null)
            return "";

        var role = RoleAccess.GetRoleById(CurrentUser.RoleId);
        return role?.Name ?? "Unknown";
    }    
}
