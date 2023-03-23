namespace IDZ3.Services.LogService
{
    /// <summary>
    /// Элемент лога
    /// </summary>
    public class LogItem
    {
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public LogItem( LogLevel logLevel, string message )
        {
            LogLevel = logLevel;
            Message = message;
            Date = DateTime.UtcNow;
        }

        public static LogItem Create( LogLevel logLevel, string message )
        {
            return new LogItem( logLevel, message );
        }
    }
}
