namespace IDZ3.Services.SourceLogService
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class LogService
    {
        // Очередь логов
        private readonly Queue<LogItem> queue = new Queue<LogItem>();

        // Инстанс сервиса логирования
        private static LogService instance = new LogService ();

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

        public void WriteLogs()
        {
            lock( queue )
            {
                List<LogItem> logs = queue.ToList();
                logs.ForEach( l =>
                {
                    ConsoleColor color = ConsoleColor.Red;
                    switch ( l.LogLevel ) {
                        case LogLevel.INFO: color = ConsoleColor.Blue; break;
                        case LogLevel.ERROR: color = ConsoleColor.Red; break;
                        case LogLevel.DEBUG: color = ConsoleColor.Yellow; break;
                        case LogLevel.STATISTICS: color = ConsoleColor.Green; break;
                    }

                    Console.ForegroundColor = color;
                    Console.WriteLine( l.ToString() );
                    Console.ResetColor();
                } );
            }
        }
    }
}
