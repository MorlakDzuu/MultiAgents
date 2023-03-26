using System.Text.Json;

namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorRecieveMessage
    {
        public VisitorActionTypes ActionType { get; set; }
        public string SerializedData { get; set; }

        public VisitorRecieveMessage( VisitorActionTypes actionType, string serializedData )
        {
            ActionType = actionType;
            SerializedData = serializedData;
        }

        public static VisitorRecieveMessage CreateVisitorGetMenuRequest( VisitorGetActualMenuMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new VisitorRecieveMessage( VisitorActionTypes.ASK_ACTUAL_MENU, jsonMessage );
        }

        public static VisitorRecieveMessage CreateVisitorGetMenuRequest( VisitorActualMenuMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new VisitorRecieveMessage( VisitorActionTypes.GET_ACTUAL_MENU, jsonMessage );
        }

        public static VisitorRecieveMessage CreateVisitorUpdateMenuRequest( VisitorMenuUpdatesMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new VisitorRecieveMessage( VisitorActionTypes.UPDATE_MENU, jsonMessage );
        }

        public static VisitorRecieveMessage CreateVisitorWaitTimeRequest( VisitorActualOrderWaitTimeMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new VisitorRecieveMessage( VisitorActionTypes.GET_ORDER_WAIT_TIME, jsonMessage );
        }

        public static VisitorRecieveMessage CreateVisitorAddToOrderRequest( int dishId )
        {
            string message = dishId.ToString();
            return new VisitorRecieveMessage( VisitorActionTypes.ADD_DISH_TO_ORDER, message );
        }

        public static VisitorRecieveMessage CreateVisitorRemoveFromOrderRequest( int dishId )
        {
            string message = dishId.ToString();
            return new VisitorRecieveMessage( VisitorActionTypes.REMOVE_DISH_FROM_ORDER, message );
        }

        public static VisitorRecieveMessage CreateVisitorMakeOrderRequest()
        {
            return new VisitorRecieveMessage( VisitorActionTypes.ASK_ADMIN_CREATE_ORDER, String.Empty );
        }
    }
}
