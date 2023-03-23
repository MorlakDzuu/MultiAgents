namespace IDZ3.Agents.Store
{
    /// <summary>
    /// Агент склада принимает файлы в таком формате
    /// </summary>
    public class StoreRecieveMessage
    {
        public StoreActionTypes ActionType { get; set; }
        public string ProductType { get; set; }
        public double ProductAmount { get; set; }
        public string DishAgentId { get; set; }

        public StoreRecieveMessage(
            StoreActionTypes actionType,
            string productType,
            double productAmount,
            string dishAgentId )
        {
            ActionType = actionType;
            ProductType = productType;
            ProductAmount = productAmount;
            DishAgentId = dishAgentId;
        }
    }
}
