namespace IDZ3.MessageContracts.Order
{
    public class OrderDishReadyMessage
    {
        public string DishAgentId { get; set; }

        public OrderDishReadyMessage( string dishAgentId )
        {
            DishAgentId = dishAgentId;
        }
    }
}
