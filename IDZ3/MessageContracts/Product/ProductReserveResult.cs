namespace IDZ3.MessageContracts.Product
{
    public class ProductReserveResult
    {
        public string DishAgentId { get; set; }
        public int ProductType { get; set; }
        public double Amount { get; set; }
        public bool Result { get; set; }

        public ProductReserveResult( string dishAgentId, int productType, double amount, bool result )
        {
            DishAgentId = dishAgentId;
            ProductType = productType;
            Amount = amount;
            Result = result;
        }
    }
}
