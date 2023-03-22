using IDZ3.Message;
using IDZ3.Services.AgentsMailService;

namespace IDZ3.Agents.Base
{
    // Базовый агент
    public class BaseAgent : IAgent
    {
        // Инстанс почтового сервиса
        private MailService _mailService;

        // Средства синхронизации
        private int _done;
        private int _stop;
        ManualResetEvent mre;

        // Стандартные свойста агента
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

            // Получем инстанс почтового сервиса
            _mailService = MailService.GetInstance();
            // Регистрируем свой почтовый ящик
            _mailService.RegisterAgentMail( Id );

            // ---
            Console.WriteLine( $"Created agent: name = {Name}, id = {Id}, owner_id = {OwnerId}\n\n" );
            // ---
        }

        ~BaseAgent()
        {
            SelfDestruct();
        }

        // Поведение агента
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

        // Приостановить агента
        public void StopWorking()
        {
            Interlocked.Exchange( ref _stop, 1 );
            mre.Reset();
        }

        // Пробуждение агента
        public void StartWorking()
        {
            Interlocked.Exchange( ref _stop, 0 );
            mre.Set();
        }

        // Самоуничтожение
        public void SelfDestruct()
        {
            Lock();

            // ---
            Console.WriteLine( $"Destroy agent: name = {Name}, id = {Id}, owner_id = {OwnerId}\n\n" );
            // ---

            Interlocked.Exchange( ref _done, 1 );
            Unlock();
        }

        // Получить сообшение по почте
        public Message<T> GetMessage<T>()
        {
            return _mailService.GetNextMessage<T>( Id );
        }

        // Отправить сообщение другому агенту
        public void SendMessageToAgent<T>( T message, string agentId )
        {
            _mailService.SendMessageToAgent<T>( message, Id, agentId );
        }

        public string GetId()
        {
            return Id;
        }

        // Захватить права владения средствами синхронизации
        protected void Lock()
        {
            while ( 1 == Interlocked.CompareExchange( ref _stop, _done, 2 ) )
            {
                mre.WaitOne();
            }
            mre.Reset();
        }

        // Отпусть захваченные средства
        protected void Unlock()
        {
            mre.Set();
        }
    }
}
