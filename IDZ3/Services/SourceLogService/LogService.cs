using IDZ3.Agents.Operation;
using IDZ3.Agents.Process;
using System.Text.Json;

namespace IDZ3.Services.SourceLogService
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class LogService
    {
        // Очередь логов
        private readonly Queue<LogItem> queue = new Queue<LogItem>();
        private readonly Queue<Process> processLogQueue = new Queue<Process>();
        private readonly Queue<Operation> operationLogQueue = new Queue<Operation>();

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

        public void AddProcessLog( Process process )
        {
            lock( processLogQueue )
            {
                processLogQueue.Enqueue( process );
            }
        } 

        public void AddOperationLog( Operation operation )
        {
            lock( operationLogQueue )
            {
                operationLogQueue.Enqueue( operation );
            }
        }

        public async void StoreProcessLogsAsync( string filePath )
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                string processLogsJson = JsonSerializer.Serialize( processLogQueue.ToList(), options );
                StreamWriter sw = new StreamWriter( filePath );
                await sw.WriteLineAsync( processLogsJson );
                sw.Close();
            } catch ( Exception ex )
            {
                LogError( $"Can't store process logs to file {filePath}; exception : {ex.Message}" );
            }
        }

        public async void StoreOperationLogsAsync( string filePath )
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                string processLogsJson = JsonSerializer.Serialize( operationLogQueue.ToList(), options );
                StreamWriter sw = new StreamWriter( filePath );
                await sw.WriteLineAsync( processLogsJson );
                sw.Close();
            }
            catch ( Exception ex )
            {
                LogError( $"Can't store process logs to file {filePath}; exception : {ex.Message}" );
            }
        }

        public async void StoreLogs( string filePath )
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            string processLogsJson = JsonSerializer.Serialize( queue.ToList(), options );
            StreamWriter sw = new StreamWriter( filePath );
            await sw.WriteLineAsync( processLogsJson );
            sw.Close();
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
