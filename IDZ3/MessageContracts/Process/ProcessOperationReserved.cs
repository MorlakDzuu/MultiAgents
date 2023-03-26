namespace IDZ3.MessageContracts.Process
{
    public class ProcessOperationReserved
    {
        public string OperationAgentId { get; set; }
        public string CookerAgentId { get; set; }
        public string EquipmentAgentId { get; set; }

        public ProcessOperationReserved( string operationAgentId, string cookerAgentId, string equipmentAgentId )
        {
            OperationAgentId = operationAgentId;
            CookerAgentId = cookerAgentId;
            EquipmentAgentId = equipmentAgentId;
        }
    }
}
