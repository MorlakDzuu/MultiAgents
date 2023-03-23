namespace IDZ3.MessageContracts.Operation
{
    public class OperationMessage
    {
        public OperationActionTypes ActionType { get; set; }
        public Agents.Operation.Operation Oper { get; set; }

        public OperationMessage( OperationActionTypes actionType, Agents.Operation.Operation oper )
        {
            ActionType = actionType;
            Oper = oper;
        }

        public static OperationMessage Create( Agents.Operation.Operation operation ) 
            => new OperationMessage( OperationActionTypes.RESERVE_COOKER_AND_EQUIP, operation );
    }
}
