using IDZ3.Message;
using System.Text.Json;

namespace IDZ3.Services.AgentsMailService
{
    /// <summary>
    /// Почтовый сервис
    /// Отправялет сообщения от одного агента к другому по Id
    /// </summary>
    public class MailService : IMailService
    {
        // Максимальное количество сообщений в очереди агента
        private readonly int MAX_MESSAGES_FOR_AGENT_IN_QUEUE = 100;

        // Событие синхронизации потоков
        private readonly ManualResetEvent mre = new ManualResetEvent( true );

        // Словарь зарегестрированных агентов
        private readonly Dictionary<string, BlockingQueue<string>> mailboxes = new Dictionary<string, BlockingQueue<string>>();

        // Инстанс почтового сервиса
        private static MailService instance = new MailService();

        protected MailService()
        {
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static MailService Instance() => instance;

        /// <summary>
        /// Регистрация почтовго ящика
        /// </summary>
        public void RegisterAgentMailbox( string agentId )
        {
            mre.WaitOne();

            if ( !mailboxes.ContainsKey( agentId ) )
            {
                mailboxes.Add( agentId, new BlockingQueue<string>( MAX_MESSAGES_FOR_AGENT_IN_QUEUE ) );

                //----
                Console.WriteLine( $"MailService: agent registered {agentId}\n\n" );
                //----
            }

            mre.Set();
        }

        public void RemoveAgentMailbox( string agentId )
        {
            mre.WaitOne();

            if ( mailboxes.ContainsKey( agentId ) )
            {
                mailboxes.Remove( agentId );
            }
        }

       /// <summary>
       /// Позволяет агенту получить следующее сообщение в очереди
       /// </summary>
        public Message<T> GetNextMessage<T>( string agentId )
        {
            RegisterAgentMailbox( agentId );
            BlockingQueue<string> agentMessages = GetAgentQueue( agentId );
            string messageString = agentMessages.PopItem();

            try
            {
                Message<T> message = JsonSerializer.Deserialize<Message<T>>( messageString );

                if ( message == null )
                {
                    throw new Exception();
                }

                return message;
            }
            catch ( Exception )
            {
                throw new Exception( "Deserialize message exception" );
            }
        }

        /// <summary>
        /// Позволяет агенту отправить сообщение другому агенту по Id
        /// </summary>
        public void SendMessageToAgent<T>( T messageContent, string agentFromId, string agentId )
        {
            Message<T> message = new Message<T>( agentFromId, messageContent );
            string messageString = JsonSerializer.Serialize( message );
            BlockingQueue<string> agentMessages = GetAgentQueue( agentId );
            agentMessages?.PushItem( messageString );

            //----
            Console.WriteLine( $"MailService: agent {agentFromId} send a message to {agentId}, message = {messageString}\n\n" );
            //----
        }

        /// <summary>
        /// Получить очередь агента
        /// </summary>
        private BlockingQueue<string> GetAgentQueue( string agentId )
        {
            mre.WaitOne();

            BlockingQueue<string>? agentMessages = mailboxes.GetValueOrDefault( agentId );

            if ( agentMessages == null )
            {
                throw new Exception( $"Agent with id = {agentId} does not exists\n\n" );
            }

            mre.Set();
            return agentMessages;
        }
    }
}
