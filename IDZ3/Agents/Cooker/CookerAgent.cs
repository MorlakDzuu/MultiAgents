using IDZ3.Agents.Base;
using IDZ3.Services.AgentsMailService;

namespace IDZ3.Agents.Cooker
{
    /// <summary>
    /// Агент повара
    /// </summary>
    public class CookerAgent : BaseAgent
    {
        // Очередь выполнения операций
        private readonly BlockingQueue<Operation.Operation> operationQueue = new BlockingQueue<Operation.Operation>( 100 );
        // Очередь остановленных операций
        private readonly Queue<Operation.Operation> stoppedOperationQueue = new Queue<Operation.Operation>();

        // Текущая операция
        private Operation.Operation currentOperation;
        // Статус текущей операции
        private CookerOperationStatus currentOperationStatus;

        // Имя повара
        private readonly string _name;

        public CookerAgent( string name, string ownerId )
            : base( AgentRoles.COOKER.ToString(), ownerId )
        {
            _name = name;
        }

        public void Action()
        {
            Lock();
            // TODO::
            Unlock();
        }

        /// <summary>
        /// Назначение новой операции
        /// </summary>
        public void PerfomOperation( Operation.Operation operation )
        {
            Lock();
            operationQueue.Enqueue( operation );
            Unlock();
        }

        /// <summary>
        /// Остановить текущуя операция
        /// </summary>
        public void StopCurrentOperation()
        {
            Lock();
            if ( currentOperationStatus == CookerOperationStatus.StartedTheOperation )
            {
                currentOperationStatus = CookerOperationStatus.OperationStopped;
                stoppedOperationQueue.Enqueue( currentOperation );
            }
            Unlock();
        }

        /// <summary>
        /// Назначить последнию приостановленную операцию
        /// </summary>
        public void PerfomStoppedOperation()
        {
            Lock();
            if ( stoppedOperationQueue.Count > 0 )
            {
                operationQueue.Enqueue( stoppedOperationQueue.Dequeue() );
            }
            Unlock();
        }
    }
}
