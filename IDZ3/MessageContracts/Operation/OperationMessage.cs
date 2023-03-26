namespace IDZ3.MessageContracts.Operation
{
    public class OperationMessage
    {
        //public OperationActionTypes ActionType { get; set; }
        public int OperType { get; set; }
        public int EquipType { get; set; }

        public OperationMessage( int operType, int equipType )
        {
            OperType = operType;
            EquipType = equipType;
        }

        public static OperationMessage Create( int operType, int equipType ) 
            => new OperationMessage( operType, equipType );
    }
}
