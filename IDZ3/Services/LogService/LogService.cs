namespace IDZ3.Services.LogService
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class LogService : ILogService
    {
        // Очередь логов
        private readonly Queue<LogItem> queue = new Queue<LogItem>();

        // Инстанс сервиса логирования
        private static LogService instance = new LogService();

        //public Task PurgeAsync(FileStream fsstream) => fsstream.WriteAsync(  )

        protected LogService()
        {
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static LogService Instance() => instance;


        private void Log( LogLevel logLevel, string message )
        {
            lock( queue )
            {
                queue.Enqueue( LogItem.Create( logLevel, message ) );
            }
        }

        public void LogInfo( string message )
        {
            Log( LogLevel.INFO, message );
        }

        public void LogError( string message )
        {
            Log( LogLevel.ERROR, message );
        }
    }
}
