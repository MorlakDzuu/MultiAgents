namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorMessage
    {
        public VisitorActionTypes ActionType { get; set; }
        public double TmeLimit { get; set; }
        public List<int> SelectedDishes { get; set; }

        public VisitorMessage( VisitorActionTypes actionType, double timeLimit, List<int> selecyedDishes )
        {
            ActionType = actionType;
            TmeLimit = timeLimit;
            SelectedDishes = selecyedDishes;
        }

        public static VisitorMessage CreateMenuRequestMessage( double timeLimit ) =>
            new VisitorMessage( VisitorActionTypes.MENU_REQUEST, timeLimit, new List<int>() );

        public static VisitorMessage CreateMakeIrderMessage( List<int> selectedItems ) =>
            new VisitorMessage( VisitorActionTypes.MAKE_ORDER, 0, selectedItems );

        public static VisitorMessage CreateOrderAgentRequestMessage() =>
            new VisitorMessage( VisitorActionTypes.GET_ORDER_AGENT, 0, new List<int>() );
    }
}
