using IDZ3.Agents.Base;
using IDZ3.Agents.Cooker;
using IDZ3.Agents.Equipment;
using IDZ3.Agents.Operation;
using IDZ3.MessageContracts.Order;
using IDZ3.MessageContracts.Process;
using IDZ3.MessagesContracts;
using System.Text.Json;

namespace IDZ3.Agents.Process
{
    public class ProcessAgent : BaseAgent
    {
        private Process _processInfo;
        private string? _orderAgentId;

        private List<OperationAgent> _operationAgents = new List<OperationAgent>();
        private Dictionary<string, CookerAgent> _operationToCooker = new Dictionary<string, CookerAgent>();
        private Dictionary<string, EquipmentAgent> _operationToEquipment = new Dictionary<string, EquipmentAgent>();

        public ProcessAgent( string ownerId ) : base( AgentRoles.PROCESS.ToString(), ownerId )
        {
            _processInfo = new Process();
            _processInfo.ProcessId = Id;
            _processInfo.ProcessOperations = new List<string>();
        }

        public new void Action()
        {
            Lock();
            Message<ProcessRecieveMessage> message = GetMessage<ProcessRecieveMessage>();
            switch ( message.MessageContent.ActionType )
            {
                case ProcessActionType.OPERATION_RESERVED:
                    ProcessOperationReserved messageCont = JsonSerializer.Deserialize<ProcessOperationReserved>( message.MessageContent.SerializedData );
                    CookerAgent cooker = (CookerAgent) _dFService.GetAgentById( messageCont.CookerAgentId );
                    EquipmentAgent equipment = ( EquipmentAgent )_dFService.GetAgentById( messageCont.EquipmentAgentId );
                    _operationToCooker.Add( messageCont.OperationAgentId, cooker );
                    _operationToEquipment.Add( messageCont.OperationAgentId, equipment );
                    if ( _operationToCooker.Count == _operationAgents.Count )
                    {
                        SendMessageToAgent<ProcessRecieveMessage>( ProcessRecieveMessage.ProcessCountWaitTime(), Id );
                    }
                    break;
                case ProcessActionType.OPERATION_FINISHED:
                    OperationAgent operationAgent = _operationAgents.First( oa => oa.Id == message.AgentFromId );
                    operationAgent.SelfDestruct();
                    _operationAgents.Remove( operationAgent );
                    _processInfo.ProcessOperations.Add( message.AgentFromId );
                    if ( _operationAgents.Count == 0 )
                    {
                        _processInfo.ProcessEnded = DateTime.UtcNow;
                        _processInfo.ProcessActive = false;
                        _loogger.LogInfo( JsonSerializer.Serialize( _processInfo ) );
                        SendMessageToAgent<OrderRecieveMessage>( OrderRecieveMessage.OrderDishIsReadyMessage( _processInfo.OrderDishId ), _orderAgentId );
                    }
                    break;
                case ProcessActionType.COUNT_WAIT_TIME:
                    List<double> times = new List<double>();
                    foreach ( OperationAgent operation in _operationAgents )
                    {
                        CookerAgent cookerAgent = _operationToCooker[ operation.Id ];
                        EquipmentAgent equipmentAgent = _operationToEquipment[ operation.Id ];
                        if ( cookerAgent.GetCurrentOperation().Id == operation.Id )
                        {
                            int currCooker = equipmentAgent.GetCurrentCookerId();
                            if ( currCooker == cookerAgent.CookerId )
                            {
                                times.Add( operation.GetOperationTime() );
                            }

                            double time = _operationToCooker.Values.First( c => c.CookerId == currCooker ).GetCurrentOperation().GetOperationTime();
                            List<int> cookersQueue = equipmentAgent.GetCurrentCookersQueue();
                            int index = cookersQueue.IndexOf( cookerAgent.CookerId );
                            for ( int i = 0; i < index; i++ )
                            {

                            }
                        }
                    }
                    break;
            }
            Unlock();
        }

        public void AddOperationAgent( OperationAgent operationAgent )
        {
            _operationAgents.Add( operationAgent );
        }

        public void StartProcess()
        {
            _processInfo.ProcessActive = true;
            _processInfo.ProcessStarted = DateTime.UtcNow;
            foreach ( OperationAgent operationAgent in _operationAgents )
            {
                operationAgent.Activate();
            }
        }

        public void SetOrderDishId( string dishId )
        {
            _processInfo.OrderDishId = dishId;
        }

        public void SetOrderAgentId( string orderAgentId )
        {
            _orderAgentId = orderAgentId;
        } 
    }
}
