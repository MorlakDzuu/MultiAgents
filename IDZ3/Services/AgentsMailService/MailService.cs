using IDZ3.Message;
using System.Text.Json;

namespace IDZ3.Services.AgentsMailService
{
    public class MailService
    {
        private readonly int MAX_MESSAGES_FOR_AGENT_IN_QUEUE = 100;

        private static MailService malServiceinstance;
        private static object locker = new();

        private Dictionary<string, BlockingQueue<string>> agentsToMessages;

        protected MailService()
        {
            agentsToMessages = new Dictionary<string, BlockingQueue<string>>();
        }

        public static MailService GetInstance()
        {
            lock ( locker )
            {
                if ( malServiceinstance == null )
                {
                    malServiceinstance = new MailService();
                }
            }

            return malServiceinstance;
        }

        public void RegisterAgentMail( string agentId )
        {
            if ( !Monitor.TryEnter( locker ) )
            {
                Monitor.Wait( locker );
            }

            if ( !agentsToMessages.ContainsKey( agentId ) )
            {
                agentsToMessages.Add( agentId, new BlockingQueue<string>( MAX_MESSAGES_FOR_AGENT_IN_QUEUE ) );
            }

            Monitor.Pulse( locker );
        }

        public Message<T> GetNextMessage<T>( string agentId )
        {
            RegisterAgentMail( agentId );
            BlockingQueue<string> agentMessages = GetAgentMessages( agentId );
            string messageString = agentMessages.Dequeue();

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

        public void SendMessageToAgent<T>( T messageContent, string agentFromId, string agentId )
        {
            Message<T> message = new Message<T>( agentFromId, messageContent );
            string messageString = JsonSerializer.Serialize( message );
            BlockingQueue<string> agentMessages = GetAgentMessages( agentId );
            agentMessages?.Enqueue( messageString );
        }

        private BlockingQueue<string> GetAgentMessages( string agentId )
        {
            if ( !Monitor.TryEnter( locker ) )
            {
                Monitor.Wait( locker );
            }

            BlockingQueue<string>? agentMessages = agentsToMessages.GetValueOrDefault( agentId );

            if ( agentMessages == null )
            {
                throw new Exception( $"Agent with id = {agentId} does not exists" );
            }

            Monitor.Pulse( locker );
            return agentMessages;
        }
    }
}
