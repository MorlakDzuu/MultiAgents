using IDZ3.MessageContracts.Operation;
using IDZ3.MessageContracts.Order;
using IDZ3.MessageContracts.Store;
using IDZ3.MessageContracts.Visitor;
using System.Text.Json;

namespace IDZ3.MessageContracts.Admin
{
    public class AdminMessage
    {
        public AgentTypesAdminUnderstand AgentType { get; set; }
        public string SerializedData { get; set; }

        public AdminMessage( AgentTypesAdminUnderstand agentType, string serializedData )
        {
            AgentType = agentType;
            SerializedData = serializedData;
        }

        public static AdminMessage VisitorCreateAdminMassege( VisitorMessage visitorMessage )
        {
            string jsonMessage = JsonSerializer.Serialize( visitorMessage );
            return new AdminMessage( AgentTypesAdminUnderstand.VISITOR, jsonMessage );
        }

        public static AdminMessage StoreCreateAdminMassege( StoreCheckResultMessage storeMessage )
        {
            string jsonMessage = JsonSerializer.Serialize( storeMessage );
            return new AdminMessage( AgentTypesAdminUnderstand.STORE, jsonMessage );
        }

        public static AdminMessage OrderCreateAdminMessage( OrderAdminMessage orderMessage )
        {
            string jsonMessage = JsonSerializer.Serialize( orderMessage );
            return new AdminMessage( AgentTypesAdminUnderstand.ORDER, jsonMessage );
        }

        public static AdminMessage OperationReserveAdminMessage( OperationMessage operationMessage )
        {
            string jsonMessage = JsonSerializer.Serialize( operationMessage );
            return new AdminMessage( AgentTypesAdminUnderstand.OPERATION, jsonMessage );
        }
    }
}
