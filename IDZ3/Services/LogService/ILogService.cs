namespace IDZ3.Services.LogService
{
    public interface ILogService
    {
        public void LogInfo( string message );
        void LogError( string message );
    }
}
