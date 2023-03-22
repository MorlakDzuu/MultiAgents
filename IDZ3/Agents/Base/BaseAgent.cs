using IDZ3.Message;
using IDZ3.Services.AgentsMailService;

namespace IDZ3.Agents.Base
{
    public class BaseAgent : IAgent
    {
        private MailService _mailService;
        private int _done;
        private int _stop;
        ManualResetEvent mre;

        public string Name { get; private set; }
        public string Id { get; private set; }
        public string OwnerId { get; private set; }

        public BaseAgent( string name, string ownerId )
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            OwnerId = ownerId;

            _stop = 0;
            _done = 0;
             mre = new ManualResetEvent( true );
            _mailService = MailService.GetInstance();
            _mailService.RegisterAgentMail( Id );

            // ---
            Console.WriteLine( $"Created agent: name = {Name}, id = {Id}, owner_id = {OwnerId}\n\n" );
            // ---
        }

        ~BaseAgent()
        {
            SelfDestruct();
        }

        public void Action()
        {
            Lock();
            Interlocked.Exchange( ref _done, 1 );
            Unlock();
        }

        public bool Done()
        {
            return 1 == Interlocked.Exchange(ref _done, 0);
        }

        public void StopWorking()
        {
            Interlocked.Exchange( ref _stop, 1 );
            mre.Reset();
        }

        public void StartWorking()
        {
            Interlocked.Exchange( ref _stop, 0 );
            mre.Set();
        }

        public void SelfDestruct()
        {
            Lock();

            // ---
            Console.WriteLine( $"Destroy agent: name = {Name}, id = {Id}, owner_id = {OwnerId}\n\n" );
            // ---

            Interlocked.Exchange( ref _done, 1 );
            Unlock();
        }

        public Message<T> GetMessage<T>()
        {
            return _mailService.GetNextMessage<T>( Id );
        }

        public void SendMessageToAgent<T>( T message, string agentId )
        {
            _mailService.SendMessageToAgent<T>( message, Id, agentId );
        }

        public string GetId()
        {
            return Id;
        }

        protected void Lock()
        {
            while ( 1 == Interlocked.CompareExchange( ref _stop, _done, 2 ) )
            {
                mre.WaitOne();
            }
            mre.Reset();
        }

        protected void Unlock()
        {
            mre.Set();
        }
    }
}
