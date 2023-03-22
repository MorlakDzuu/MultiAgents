namespace IDZ3.Agents.Store
{
    public enum StoreActionTypes
    {
        CHECK_PRODUCT = 0,
        RESERVE_PRODUCT = 1,
        DISH_READY = 2,
        CANCEL_PRODUCT = 3
    }

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
