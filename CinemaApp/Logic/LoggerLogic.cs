public class LoggerLogic //singleton
{
    private static readonly Lazy<LoggerLogic> instance = new Lazy<LoggerLogic>(() => new LoggerLogic());
    public static bool LoggingEnabled = true;

    private LoggerLogic()
    {
        if (!LoggingEnabled)
            return;

        LoggerAccess.CreateDirectory();

        LogInternal($"--- Logger started at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---");
    }

    public static LoggerLogic Instance => instance.Value;

    public void Log(string message)
    {
        if (!LoggingEnabled)
            return;
            
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"{timestamp} | {message}";
        LogInternal(logEntry);
    }

    private void LogInternal(string logEntry)
    {
        string dailyLogFilePath = LoggerAccess.GetDailyLogFilePath();

        if (File.Exists(dailyLogFilePath))
        {
            if (logEntry.Contains("--- Logger started at"))
            {
                File.AppendAllText(dailyLogFilePath, Environment.NewLine);
            }
        }

        // Append the log entry
        File.AppendAllText(dailyLogFilePath, logEntry + Environment.NewLine);
    }
}
