using IDZ3.Agents.Base;
using IDZ3.MessageContracts.Order;
using IDZ3.MessageContracts.Product;

namespace IDZ3.Agents.Product
{
    /// <summary>
    /// Агент продукта
    /// </summary>
    public class ProductAgent : OneBehaviorBaseAgent
    {
        string _dishId;
        string _orderId;
        // Тип продукта
        int _productType;
        // Объем продукта
        double _productAmount;

        private ManualResetEvent _manualReset;

        public ProductAgent(
            string ownerId,
            string dishId,
            string orderId,
            int productType )
            : base( AgentRoles.PRODUCT.ToString() , ownerId )
        {
            _dishId = dishId;
            _orderId = orderId;
            _productType = productType;
            _productAmount = 0;
            _manualReset = new ManualResetEvent( false );
        }

        public void Action()
        {
            _manualReset.WaitOne();
            bool reserveResult = _productAmount > 0;
            ProductReserveResult productReserveResult = new ProductReserveResult( _dishId, _productType, _productAmount, reserveResult );
            SendMessageToAgent<OrderRecieveMessage>( OrderRecieveMessage.ProductReserveMessage( productReserveResult ), _orderId );
        }

        /// <summary>
        /// Получить тип продукта
        /// </summary>
        public int GetProductType()
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

        public void Reserve()
        {
            _manualReset.Set();
        }
    }
}
