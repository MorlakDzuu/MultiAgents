using IDZ3.Agents.Base;
using IDZ3.Agents.Operation;
using IDZ3.DFs.DFCookers;

namespace IDZ3.Agents.Cooker
{
    /// <summary>
    /// Агент повара
    /// </summary>
    public class CookerAgent : BaseAgent
    {
        // Очередь выполнения операций
        private readonly Queue<OperationAgent> operationQueue = new Queue<OperationAgent>();
        // Очередь остановленных операций
        private readonly Queue<OperationAgent> stoppedOperationQueue = new Queue<OperationAgent>();

        // Текущая операция
        private OperationAgent currentOperation;
        // Статус текущей операции
        private CookerOperationStatus currentOperationStatus;

        private ManualResetEvent _manualReset;

        // Информация о поваре
        public int CookerId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public CookerAgent( CookerRes cookerInfo, string ownerId )
            : base( AgentRoles.COOKER.ToString(), ownerId )
        {
            CookerId = cookerInfo.Id;
            Name = cookerInfo.Name;
            Active = cookerInfo.Active;
            currentOperationStatus = CookerOperationStatus.Chilling;
            _manualReset = new ManualResetEvent( false );
        }

        public new void Action()
        {
            Lock();
            switch ( currentOperationStatus ) { 
                case CookerOperationStatus.Chilling:
                    if ( operationQueue.Count == 0 )
                    {
                        _manualReset.WaitOne();
                    }
                    currentOperation = operationQueue.Dequeue();
                    while ( currentOperation.GetEquipmentAgent().GetCurrentCookerId() != CookerId )
                    {
                        Thread.Sleep( 100 );
                    }
                    currentOperation.StartOperation();
                    currentOperationStatus = CookerOperationStatus.StartedTheOperation;
                    _manualReset.Reset();
                    break;
                case CookerOperationStatus.StartedTheOperation:
                    if ( currentOperation.GetEndDate() <= DateTime.UtcNow )
                    {
                        currentOperation.FinishOperation();
                        currentOperation.GetEquipmentAgent().CookerFinish();
                        currentOperationStatus = CookerOperationStatus.PerfomedTheOperation;
                    }
                    break;
                case CookerOperationStatus.PerfomedTheOperation:
                    currentOperation = null;
                    currentOperationStatus = CookerOperationStatus.Chilling;
                    break;
            }

            Unlock();
        }

        /// <summary>
        /// Назначение новой операции
        /// </summary>
        public void PerfomOperation( OperationAgent operation )
        {
            Lock();
            operation.SetCookerId( CookerId );
            operationQueue.Enqueue( operation );
            Unlock();
            _manualReset.Set();
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

        public int GetOperationCount()
        {
            int count = 0;
            Lock();
            count = currentOperation == null ? 0 : 1;
            count += operationQueue.Count;
            Unlock();
            return count;
        }

        public List<OperationAgent> GetCurrentQueue()
        {
            return operationQueue.ToList();
        }

        public OperationAgent GetCurrentOperation()
        {
            return currentOperation;
        }
    }
}
