using IDZ3.Agents.Base;
using IDZ3.Agents.Operation;
using IDZ3.DFs.DFEquipment;

namespace IDZ3.Agents.Equipment
{
    public class EquipmentAgent : OneBehaviorBaseAgent
    {
        private readonly Queue<OperationAgent> _operationsQueue = new Queue<OperationAgent>();
        private OperationAgent _currentOperationAgent;
       // private OperationAgent currentOperation;

        public int EquipId { get; set; }
        public int EquipType { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public EquipmentAgent( Oper equipInfo , string ownerId ) : base( AgentRoles.EQUIPMENT.ToString(), ownerId )
        {
            EquipId = equipInfo.Id;
            EquipType = equipInfo.Type;
            Name = equipInfo.Name;
            Active = equipInfo.Active;

            _currentOperationAgent = null;
        }

        public OperationAgent GetCurrentOperationAgent()
        {
            return _currentOperationAgent;
        }

        public List<OperationAgent> GetCurrentOperationsQueue()
        {
            return _operationsQueue.ToList();
        }

        public void AddOperation( OperationAgent operation )
        {
            lock ( _operationsQueue )
            {
                if ( _currentOperationAgent == null )
                {
                    _currentOperationAgent = operation;
                    return;
                }

                _operationsQueue.Enqueue( operation );
            }
        }

        public void CookerFinish()
        {
            lock ( _operationsQueue )
            {
                if ( _operationsQueue.Count == 0 )
                {
                    _currentOperationAgent = null;
                    return;
                }

                _currentOperationAgent = _operationsQueue.Dequeue();
            }
        }

        public int GetOperationsCount()
        {
            lock( _operationsQueue )
            {
                return _operationsQueue.Count + ( _currentOperationAgent == null ? 0 : 1 );
            }
        }
    }
}
