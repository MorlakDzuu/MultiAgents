using IDZ3.Agents.Base;

namespace IDZ3.Agents.Product
{
    /// <summary>
    /// Агент продукта
    /// </summary>
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
            : base( AgentRoles.PRODUCT.ToString() , ownerId )
        {
            _productType = productType;
            _productAmount = productAmount;
            _productAmount = productAmount;
        }

        /// <summary>
        /// Получить тип продукта
        /// </summary>
        public string GetType()
        {
            return _productType;
        }

        /// <summary>
        /// Установить новый объем продукта
        /// </summary>
        public void SetAmount( double productAmount )
        {
            _productAmount = productAmount;
        }

        /// <summary>
        /// Получить текущий объем продукта
        /// </summary>
        public double GetAmount()
        {
            return _productAmount;
        }
    }
}
