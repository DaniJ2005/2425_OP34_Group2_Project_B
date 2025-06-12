using System.Text.Json;

public static class SessionDataLogic
{
    private const string UnicodeFlag = "unicode_test_passed";
    private const string AuthFlag = "auth";

    // Symbol Check
    public static bool HasPassedSymbolCheck() => IsSet(UnicodeFlag);
    public static void MarkSymbolCheckPassed() => Set(UnicodeFlag);

    // Authentication
    public static bool IsAuthenticated()
    {
        if (!IsSet(AuthFlag)) return false;

        var content = Get(AuthFlag);
        var flag = JsonSerializer.Deserialize<FlagData>(content!);
        return flag?.Authenticated == true;
    }

    public static void MarkAuthenticated(User user)
    {
        var flag = new FlagData
        {
            Email = user.Email,
            Password = user.Password,
            Authenticated = true
        };

        Set(AuthFlag, JsonSerializer.Serialize(flag));
    }

    public static void Logout()
    {
        Unset(AuthFlag);
    }

    public static void TryAutoLogin()
    {
        if (!IsAuthenticated()) return;

        var json = Get(AuthFlag);
        var flag = JsonSerializer.Deserialize<FlagData>(json!);
        if (flag == null || !flag.Authenticated) return;

        var user = UserAccess.GetByEmail(flag.Email ?? "");
        if (user == null) return;

        UserLogic.Login(flag.Email, flag.Password, true);

        if (UserLogic.CurrentUser != null)
            LoggerLogic.Instance.Log($"Auto-login successful | Email: {user.Email}");
    }

    private static bool IsSet(string flagName) => SessionDataAccess.Exists(flagName);
    private static void Set(string flagName, string contents = "true") => SessionDataAccess.Write(flagName, contents);
    private static void Unset(string flagName) => SessionDataAccess.Delete(flagName);
    private static string? Get(string flagName) => SessionDataAccess.Exists(flagName)
        ? File.ReadAllText(SessionDataAccess.GetPath(flagName))
        : null;
}
