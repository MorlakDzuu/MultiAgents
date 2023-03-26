namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorAdminMessage
    {
        public VisitorAdminActionTypes ActionType { get; set; }
        public double TimeLimit { get; set; }
        public List<int> SelectedDishesIds { get; set; }

        public VisitorAdminMessage( VisitorAdminActionTypes actionType, double timeLimit, List<int> selectedDishesIds )
        {
            ActionType = actionType;
            TimeLimit = timeLimit;
            SelectedDishesIds = selectedDishesIds;
        }

        public static VisitorAdminMessage CreateMenuRequestMessage( double timeLimit ) =>
            new VisitorAdminMessage( VisitorAdminActionTypes.MENU_REQUEST, timeLimit, new List<int>() );

        public static VisitorAdminMessage CreateMakeOrderMessage( List<int> selectedItems ) =>
            new VisitorAdminMessage( VisitorAdminActionTypes.MAKE_ORDER, 0, selectedItems );

        public static VisitorAdminMessage CreateOrderAgentRequestMessage() =>
            new VisitorAdminMessage( VisitorAdminActionTypes.GET_ORDER_AGENT, 0, new List<int>() );
    }
}
