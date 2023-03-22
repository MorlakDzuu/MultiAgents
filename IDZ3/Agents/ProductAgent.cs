using IDZ3.Agents.Base;

namespace IDZ3.Agents
{
    // Агент продукта
    public class ProductAgent : BaseAgent
    {
        string _productType;
        double _productAmount;

        public ProductAgent(
            string ownerId,
            string productType,
            double productAmount )
            : base( "PRODUCT", ownerId )
        {
            _productType = productType;
            _productAmount = productAmount;
            _productAmount = productAmount;
        }
        
        new public void Action()
        {
            Lock();
            Thread.Sleep( 100 );
            Unlock();
        }

        public string GetType()
        {
            return _productType;
        }

        public void SetAmount( double productAmount )
        {
            _productAmount = productAmount;
        }

        public double GetAmount()
        {
            return _productAmount;
        }
    }
}
