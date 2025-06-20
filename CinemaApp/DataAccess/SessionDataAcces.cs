public static class SessionDataAccess
{
    private static readonly string _directory = GetSessionDataDirectory();

    private static string GetSessionDataDirectory()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(baseDir, @"..\..\..\DataSources\SessionData");
        return Path.GetFullPath(relativePath);
    }

    public static void CreateDirectory() => Directory.CreateDirectory(_directory);

    public static string GetPath(string flagName) => Path.Combine(_directory, $"{flagName}.flag");

    public static bool Exists(string flagName) => File.Exists(GetPath(flagName));

    public static void Write(string flagName, string contents = "true")
    {
        CreateDirectory();
        File.WriteAllText(GetPath(flagName), contents);
    }

    public static void Delete(string flagName)
    {
        string path = GetPath(flagName);
        if (File.Exists(path)) File.Delete(path);
    }
}
