namespace IDZ3.DFs.DFOperations
{
    public class DFOperation
    {
        private static OperationList _kitchenOperationTypes;

        public static void SetValue( OperationList kitchenOperationTypes )
        {
            _kitchenOperationTypes = kitchenOperationTypes;
        }

        public static OperationList GetValue()
        {
            return _kitchenOperationTypes;
        }
    }
}
