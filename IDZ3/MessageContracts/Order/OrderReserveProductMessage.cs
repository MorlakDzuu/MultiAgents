namespace IDZ3.MessageContracts.Order
{
    public class OrderReserveProductMessage
    {
        public int ProductType { get; set; }
        public double Amount { get; set; }
        public string DishAgentId { get; set; }

        public OrderReserveProductMessage( int productType, double amount, string dishAgentId )
        {
            ProductType = productType;
            Amount = amount;
            DishAgentId = dishAgentId;
        }

        public static OrderReserveProductMessage Create( int type, double amount, string agentId ) => 
            new OrderReserveProductMessage( type, amount, agentId );
    }
}
