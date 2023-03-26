using IDZ3.MessagesContracts;
using IDZ3.Services.ActiveAgentsDFService;
using IDZ3.Services.AgentsMailService;
using IDZ3.Services.SourceLogService;

namespace IDZ3.Agents.Base
{
    /// <summary>
    /// Базовый агент
    /// </summary>
    public class BaseAgent : IAgent
    {
        // Инстанс почтового сервиса
        private MailService _mailService;
        // Инстас координатора
        protected DFService _dFService;

        protected LogService _loogger;

        // Средства синхронизации
        private int _done;
        private int _stop;
        private ManualResetEvent mre;

        // Стандартные свойста агента
        public string ServiceType { get; private set; }
        public string Id { get; private set; }
        public string OwnerId { get; private set; }

        public BaseAgent( string serviceType, string ownerId )
        {
            ServiceType = serviceType;
            Id = Guid.NewGuid().ToString();
            OwnerId = ownerId;

            _stop = 0;
            _done = 0;
             mre = new ManualResetEvent( true );

            _loogger = LogService.Instance();
            // Получем инстанс почтового сервиса
            _mailService = MailService.Instance();
            // Регистрируем свой почтовый ящик
            _mailService.RegisterAgentMailbox( Id );
            // Получаем инстанс координатора
            _dFService = DFService.GetInstance();
            // Регистрируемся
            _dFService.RegisterAgent( AgentProfile.Create( serviceType, Id, ownerId ), this );
        }

        ~BaseAgent()
        {
            SelfDestruct();
        }

        /// <summary>
        /// Поведение агента
        /// </summary>
        public void Action()
        {
            Lock();
            Interlocked.Exchange( ref _done, 1 );
            Unlock();
        }

        /// <summary>
        /// Нужно ли повторить поведение агента
        /// </summary>
        public bool Done()
        {
            return 1 == Interlocked.Exchange(ref _done, 0);
        }

        /// <summary>
        /// Приостановить агента
        /// </summary>
        public void StopWorking()
        {
            Interlocked.Exchange( ref _stop, 1 );
            mre.Reset();
        }

        /// <summary>
        /// Пробуждение агента
        /// </summary>
        public void StartWorking()
        {
            Interlocked.Exchange( ref _stop, 0 );
            mre.Set();
        }

        /// <summary>
        /// Самоуничтожение
        /// </summary>
        public void SelfDestruct()
        {
            Lock();
            _mailService.RemoveAgentMailbox( Id );
            _dFService.DeregesterAgent( Id );
            Interlocked.Exchange( ref _done, 1 );
            Unlock();
        }

        /// <summary>
        /// Получить сообшение по почте
        /// </summary>
        public Message<T> GetMessage<T>()
        {
            return _mailService.GetNextMessage<T>( Id );
        }

        /// <summary>
        /// Отправить сообщение другому агенту
        /// </summary>
        public void SendMessageToAgent<T>( T message, string agentId )
        {
            _mailService.SendMessageToAgent<T>( message, Id, agentId );
        }

        public string GetId()
        {
            return Id;
        }

        /// <summary>
        /// Захватить права владения средствами синхронизации
        /// </summary>
        protected void Lock()
        {
            while ( 1 == Interlocked.CompareExchange( ref _stop, _done, 2 ) )
            {
                mre.WaitOne();
            }
            mre.Reset();
        }

        /// <summary>
        /// Отпусть захваченные средства
        /// </summary>
        protected void Unlock()
        {
            mre.Set();
        }
    }
}
