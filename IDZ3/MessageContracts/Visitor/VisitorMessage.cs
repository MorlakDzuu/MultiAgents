namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorMessage
    {
        public VisitorActionTypes ActionType { get; set; }
        public double TimeLimit { get; set; }
        public List<int> SelectedDishesIds { get; set; }

        public VisitorMessage( VisitorActionTypes actionType, double timeLimit, List<int> selectedDishesIds )
        {
            ActionType = actionType;
            TimeLimit = timeLimit;
            SelectedDishesIds = selectedDishesIds;
        }

        public static VisitorMessage CreateMenuRequestMessage( double timeLimit ) =>
            new VisitorMessage( VisitorActionTypes.MENU_REQUEST, timeLimit, new List<int>() );

        public static VisitorMessage CreateMakeOrderMessage( List<int> selectedItems ) =>
            new VisitorMessage( VisitorActionTypes.MAKE_ORDER, 0, selectedItems );

        public static VisitorMessage CreateOrderAgentRequestMessage() =>
            new VisitorMessage( VisitorActionTypes.GET_ORDER_AGENT, 0, new List<int>() );
    }
}
