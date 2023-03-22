using IDZ3.Agents.Base;

namespace IDZ3.Agents
{
    // Агент продукта
    public class ProductAgent : BaseAgent
    {
        string _productType;
        double _productAmount;

        public ProductAgent( string ownerId, string productType ) : base( "PRODUCT", ownerId )
        {
            _productType = productType;
        }
        
        new public void Action()
        {
            Lock();
            Thread.Sleep( 100 );
            Unlock();
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
