[TestClass]
public class LoggerTest
{
    private string? dailyLogFilePath = LoggerAccess.GetDailyLogFilePath();

    [TestInitialize]
    public void TestInitialize()
    {
        // Ensure a fresh start for each test by deleting existing log files
        if (File.Exists(dailyLogFilePath))
        {
            File.Delete(dailyLogFilePath);
        }
    }

    [TestMethod]
    public void Log_ShouldLogCorrectly()
    {
        // Arrange
        string expectedLog = "Movie selected | ID: 123 | Title: Inception";

        // Act
        LoggerLogic.Instance.Log(expectedLog);

        // Assert
        Assert.IsTrue(File.ReadAllText(dailyLogFilePath).Contains(expectedLog));
    }

    [TestMethod]
    public void LogLocation_ShouldBeCorrect()
    {
        // Arrange
        string expectedDirectory = GetLogPath();
        string logEntry = "Movie selected | ID: 123 | Title: Inception";

        // Act
        LoggerLogic.Instance.Log(logEntry);

        // Assert
        Assert.IsTrue(File.Exists(dailyLogFilePath));
        Assert.IsTrue(Path.GetDirectoryName(dailyLogFilePath).Equals(expectedDirectory));
    }

    [TestMethod]
    public void LogTimestamp_ShouldBeIncluded()
    {
        // Arrange
        string expectedTimestampFormat = "yyyy-MM-dd HH:mm:ss";
        string logEntry = "Movie selected | ID: 123 | Title: Inception";

        // Act
        LoggerLogic.Instance.Log(logEntry);
        string logContent = File.ReadAllText(dailyLogFilePath);

        // Assert
        string timestampInLog = logContent.Split('|')[0].Trim();
        DateTime logTimestamp;
        bool isValidTimestamp = DateTime.TryParseExact(timestampInLog, expectedTimestampFormat, null, System.Globalization.DateTimeStyles.None, out logTimestamp);

        Assert.IsTrue(isValidTimestamp);
    }

    [TestMethod]
    public void LogAccessibility_ShouldBeAvailable()
    {
        // Arrange
        string logDirectory = GetLogPath();
        string logEntry = "Movie selected | ID: 123 | Title: Inception";

        // Act
        LoggerLogic.Instance.Log(logEntry);
        string content = File.ReadAllText(dailyLogFilePath);

        // Assert
        Assert.IsTrue(Directory.Exists(logDirectory));
        Assert.IsTrue(File.Exists(dailyLogFilePath));
        Assert.IsTrue(content.Contains(logEntry), "Test log file should contain the log entry.");
    }
    
    [TestMethod]
    public void SingletonInstance_ShouldReturnSameObject()
    {
        // Act
        LoggerLogic instance1 = LoggerLogic.Instance;
        LoggerLogic instance2 = LoggerLogic.Instance;

        // Assert
        Assert.AreSame(instance1, instance2, "LoggerLogic.Instance should return the same instance (singleton).");
    }

    [TestMethod]
    public void Log_ShouldAppendLogEntryWithCorrectFormatting()
    {
        // Arrange
        string testMessage = "Log message with specific format ";
        string expectedLogFormat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + testMessage;

        // Act
        LoggerLogic.Instance.Log(testMessage);

        // Assert
        string content = File.ReadAllText(dailyLogFilePath);
        Assert.IsTrue(content.Contains(expectedLogFormat), "Test log file should contain the log entry in the correct format.");
    }


    //helper metods
    private static string GetLogPath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string logPath = Path.Combine(baseDirectory, @"..\..\..\DataSources\Logs");

        return Path.GetFullPath(logPath);
    }
}
