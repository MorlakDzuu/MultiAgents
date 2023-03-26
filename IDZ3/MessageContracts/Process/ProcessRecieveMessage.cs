using System.Text.Json;

namespace IDZ3.MessageContracts.Process
{
    public class ProcessRecieveMessage
    {
        public ProcessActionType ActionType { get; set; }
        public string SerializedData { get; set; }

        public ProcessRecieveMessage( ProcessActionType actionType, string serializedData )
        {
            ActionType = actionType;
            SerializedData = serializedData;   
        }

        public static ProcessRecieveMessage ProcessOperationReservedMessage( ProcessOperationReserved processOperationReserved )
        {
            string jsonMessage = JsonSerializer.Serialize( processOperationReserved );
            return new ProcessRecieveMessage( ProcessActionType.OPERATION_RESERVED, jsonMessage );
        }

        public static ProcessRecieveMessage ProcessOperationFinished() => 
            new ProcessRecieveMessage( ProcessActionType.OPERATION_FINISHED, String.Empty );

        public static ProcessRecieveMessage ProcessCountWaitTime() =>
            new ProcessRecieveMessage( ProcessActionType.COUNT_WAIT_TIME, String.Empty );
    }
}
