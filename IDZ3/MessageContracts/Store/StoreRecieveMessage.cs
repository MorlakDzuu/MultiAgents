namespace IDZ3.MessageContracts.Store
{
    /// <summary>
    /// Агент склада принимает файлы в таком формате
    /// </summary>
    public class StoreRecieveMessage
    {
        public StoreActionTypes ActionType { get; set; }
        public int ProductType { get; set; }
        public double ProductAmount { get; set; }
        public string OrderAgentId { get; set; }
        public string DishAgentId { get; set; }

        public StoreRecieveMessage(
            StoreActionTypes actionType,
            int productType,
            double productAmount,
            string orderAgentId,
            string dishAgentId )
        {
            ActionType = actionType;
            ProductType = productType;
            ProductAmount = productAmount;
            OrderAgentId = orderAgentId;
            DishAgentId = dishAgentId;
        }
    }
}
