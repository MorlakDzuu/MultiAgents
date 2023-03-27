using IDZ3.Agents.Base;
using IDZ3.Agents.Equipment;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Operation;
using IDZ3.MessageContracts.Process;
using System.Text.Json;

namespace IDZ3.Agents.Operation
{
    /// <summary>
    /// Агент операции процесса
    /// </summary>
    public class OperationAgent : OneBehaviorBaseAgent
    {
        // Операция процесса
        private Operation _operation;

        private EquipmentAgent? _equipmentAgent;

        int _operationType;
        int _equipmentType;
        double _time;

        private ManualResetEvent _manualReset;

        public OperationAgent(
            int operationType,
            int equipmentType,
            double time,
            int cardId,
            string processId,
            string ownerId )
            : base( AgentRoles.OPERATION.ToString(), ownerId )
        {
            _operationType = operationType;
            _equipmentType = equipmentType;
            _time = time;
            _operation = new Operation();
            _operation.OperId = Id;
            _operation.OperCardId = cardId;
            _operation.OperProcessId = processId;
            _manualReset = new ManualResetEvent( false );
        }

        /// <summary>
        /// Запрашивает у управляющего агента резервирование повара
        /// и кухонного оборудования для выполнеия операции
        /// </summary>
        public new void Action()
        {
            _manualReset.WaitOne();
            SendMessageToAgent<AdminMessage>(
                AdminMessage.OperationReserveAdminMessage( OperationMessage.Create( _operationType, _equipmentType ) ), OwnerId );
        }

        public void StartOperation()
        {
            _operation.OperStarted = DateTime.UtcNow;
            _operation.OperActive = true;
        }

        public void FinishOperation()
        {
            _operation.OperEnded = DateTime.UtcNow;
            _operation.OperActive = false;
            SendMessageToAgent<ProcessRecieveMessage>( ProcessRecieveMessage.ProcessOperationFinished(), _operation.OperProcessId );
            _loogger.AddOperationLog( _operation );
        }

        public DateTime? GetEndDate()
        {
            return _operation.OperStarted?.AddSeconds( _time );
        }

        public void Activate()
        {
            _manualReset.Set();
        }

        public void SetCookerId( int cookerId )
        {
            _operation.OperCookerId = cookerId;
        }

        public int GetOperationType()
        {
            return _operationType;
        }

        public int GetEquipmentType()
        {
            return _equipmentType;
        }

        public void SetEquipmentAgent( EquipmentAgent equipmentAgent )
        {
            _equipmentAgent = equipmentAgent;
            _operation.OperEquipId = equipmentAgent.EquipId;
        }

        public EquipmentAgent GetEquipmentAgent()
        {
            return _equipmentAgent;
        }

        public string GetProcessId()
        {
            return _operation.OperProcessId;
        }

        public double GetOperationTime()
        {
            return _time;
        }
    }
}
