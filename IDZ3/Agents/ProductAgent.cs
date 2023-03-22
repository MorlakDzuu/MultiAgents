using IDZ3.Agents.Base;

namespace IDZ3.Agents
{
    // Агент продукта
    public class ProductAgent : BaseAgent
    {
        // Тип продукта
        string _productType;
        // Объем продукта
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
        
        // Продукт просто существует
        new public void Action()
        {
            Lock();
            Thread.Sleep( 100 );
            Unlock();
        }

        // Получить тип продукта
        public string GetType()
        {
            return _productType;
        }

        // Установить новый объем продукта
        public void SetAmount( double productAmount )
        {
            _productAmount = productAmount;
        }
        
        // Получить текущий объем продукта
        public double GetAmount()
        {
            return _productAmount;
        }
    }
}
