using IDZ3.DFs.DFEquipmentType;
using IDZ3.DFs.DFOperations;

namespace IDZ3.Agents.Operation
{
    public class Operation
    {
        public OperationType OperType { get; set; }
        public EquipmentType EquipType { get; set; }

        public Operation( OperationType operType, EquipmentType equipType )
        {
            OperType = operType;
            EquipType = equipType;
        }

        public static Operation Create( OperationType operationType, EquipmentType equipmentType )
            => new Operation( operationType, equipmentType );
    }
}
