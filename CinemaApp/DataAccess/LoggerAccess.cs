public static class LoggerAccess
{
    private static readonly string _logPath = GetLogPath();

    private static string GetLogPath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string logPath = Path.Combine(baseDirectory, @"..\..\..\DataSources\Logs");

        return Path.GetFullPath(logPath);
    }

    public static void CreateDirectory()
    {
        Directory.CreateDirectory(_logPath);
    }

    public static string GetDailyLogFilePath()
    {
        string baseLogDirectory = GetLogPath();
        string fileName = $"reservation_log_{DateTime.Now:yyyy-MM-dd}.txt";

        return Path.Combine(baseLogDirectory, fileName);
    }
}
