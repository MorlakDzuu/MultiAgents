namespace IDZ3.MessageContracts.Store
{
    public class StoreCheckResultMessage
    {
        public int ProductType { get; set; }
        public double Amount { get; set; }
        public string DishId { get; set; }
        public string OrderId { get; set; }
        public bool Result { get; set; }

        public StoreCheckResultMessage( int productType, double amount, string dishId, string orderId, bool result )
        {
            ProductType = productType;
            Amount = amount;
            DishId = dishId;
            OrderId = orderId;
            Result = result;
        }

        public static StoreCheckResultMessage Create( int productType, double amount, string dishId, string orderId, bool result ) =>
            new StoreCheckResultMessage( productType, amount, dishId, orderId, result );
    }
}
