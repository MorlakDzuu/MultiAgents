namespace IDZ3.Agents.Store
{
    public enum StoreActionTypes
    {
        CHECK = 0,
        RESERVE = 1
    }

    public class StoreRecieveMessage
    {
        public StoreActionTypes Type { get; set; }
        public string ProductName { get; set; }
        public double ProductAmount { get; set; }

        public StoreRecieveMessage(
            StoreActionTypes type,
            string productName,
            double productAmount )
        {
            Type = type;
            ProductName = productName;
            ProductAmount = productAmount;
        }
    }
}
