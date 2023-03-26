using IDZ3.Agents.Base;
using IDZ3.Agents.Operation;
using IDZ3.DFs.DFEquipment;

namespace IDZ3.Agents.Equipment
{
    public class EquipmentAgent : OneBehaviorBaseAgent
    {
        private readonly Queue<int> cookersQueue = new Queue<int>();
        //private readonly Queue<OperationAgent> operations = new Queue<OperationAgent>();
        private int _currentCookerId;
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

            _currentCookerId = -1;
        }

        public int GetCurrentCookerId()
        {
            return _currentCookerId;
        }

        public List<int> GetCurrentCookersQueue()
        {
            return cookersQueue.ToList();
        }

        public void AddCooker( int cookerId )
        {
            lock ( cookersQueue )
            {
                if ( _currentCookerId == -1 )
                {
                    _currentCookerId = cookerId;
                    return;
                }

                cookersQueue.Enqueue( cookerId );
            }
        }

        public void CookerFinish()
        {
            lock ( cookersQueue )
            {
                if ( cookersQueue.Count == 0 )
                {
                    _currentCookerId = -1;
                    return;
                }

                _currentCookerId = cookersQueue.Dequeue();
            }
        }

        public int GetCookersCount()
        {
            lock( cookersQueue )
            {
                return cookersQueue.Count + ( _currentCookerId == -1 ? 0 : 1 );
            }
        }
    }
}
