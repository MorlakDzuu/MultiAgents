using IDZ3.MessageContracts.Product;
using System.Text.Json;

namespace IDZ3.MessageContracts.Order
{
    public class OrderRecieveMessage
    {
        public OrderActionTypes ActionType { get; set; }
        public string SerializedData { get; set; }

        public OrderRecieveMessage( OrderActionTypes actionType, string serializedData )
        {
            ActionType = actionType;
            SerializedData = serializedData;
        }

        public static OrderRecieveMessage ProductReserveMessage( ProductReserveResult message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new OrderRecieveMessage( OrderActionTypes.PRODUCT_RESERVE_RESULT, jsonMessage );
        }

        public static OrderRecieveMessage ProductsReservesMessage() => new OrderRecieveMessage( OrderActionTypes.PRODUCTS_RESERVED, String.Empty );

        public static OrderRecieveMessage OrderDishIsReadyMessage( string dishId )
        {
            string jsonMessage = JsonSerializer.Serialize( new OrderDishReadyMessage( dishId ) );
            return new OrderRecieveMessage( OrderActionTypes.DISH_IS_READY, jsonMessage );
        }

        public static OrderRecieveMessage ProcessWaitTimeMessage( double time )
        {
            string jsonMessage = time.ToString();
            return new OrderRecieveMessage( OrderActionTypes.PROCESS_WAIT_TIME, jsonMessage );
        }
    }
}
