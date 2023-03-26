using System.Text.Json;

namespace IDZ3.MessageContracts.Order
{
    public class OrderAdminMessage
    {
        public OrderAdminMessageType AdminMessageType { get; set; }
        public string SerializedData { get; set; }

        public OrderAdminMessage( OrderAdminMessageType adminMessageType, string serializedData )
        {
            AdminMessageType = adminMessageType;
            SerializedData = serializedData;
        }

        public static OrderAdminMessage OrderAdminReserveProductMessage( OrderReserveProductMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new OrderAdminMessage( OrderAdminMessageType.RESERVE_PRODUCT, jsonMessage );
        }

        public static OrderAdminMessage OrderAdminDishIsReadyMessage( OrderDishReadyMessage message )
        {
            string jsonMessage = JsonSerializer.Serialize( message );
            return new OrderAdminMessage( OrderAdminMessageType.DISH_IS_READY, jsonMessage );
        }
    }
}
