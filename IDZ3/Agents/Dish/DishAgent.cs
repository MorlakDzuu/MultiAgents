using IDZ3.Agents.Base;
using IDZ3.Agents.Process;
using IDZ3.Agents.Product;
using IDZ3.DFs.DFDishCards;

namespace IDZ3.Agents.Dish
{
    public class DishAgent : BaseAgent
    {
        private ProcessAgent _processAgent;
        private List<Prod> _productsList;
        private List<ProductAgent> _productAgents;

        public DishAgent( ProcessAgent processAgent, List<Prod> productsList, string ownerId ) : base( "MENU_ITEM", ownerId )
        {
            _processAgent = processAgent;
            _productsList = productsList;
            _productAgents = new List<ProductAgent>();

            _processAgent.SetOrderDishId( Id );
        }

        public List<Prod> GetProductsList()
        {
            return _productsList;
        }

        public void AddProductAgentId( string productAgentId )
        {
            ProductAgent productAgent = (ProductAgent) _dFService.GetAgentById( productAgentId );
            _productAgents.Add( productAgent );
        }

        public ProcessAgent GetProcessAgent()
        {
            return _processAgent;
        }

        public new void SelfDestruct()
        {
            _productAgents.ForEach( pa => pa.SelfDestruct() );
            _processAgent.SelfDestruct();
            base.SelfDestruct();
        }
    }
}
