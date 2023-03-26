using IDZ3.Agents.Base;
using IDZ3.DFs.DFDishCards;
using IDZ3.DFs.DFMenu;
using IDZ3.MessageContracts.Visitor;

namespace IDZ3.Agents.Menu
{
    public class MenuAgent : BaseAgent
    {
        private List<MenuDish> _menuDishes;
        private List<DishCard> _dishCards;
        private Dictionary<int, double> _store;
        private ManualResetEvent _storeUpdated;
        private List<MenuDish> _currentChanges;
        private List<string> _visitorSsubscribers;

        public MenuAgent( string ownerId ) : base( AgentRoles.MENU.ToString(), ownerId )
        {
            _currentChanges = new List<MenuDish>();
            _visitorSsubscribers = new List<string>();
            // loaded from file
            _dishCards = DFs.DFDishCards.DFDishCards.GetValue().DishCards;
            _menuDishes = DFs.DFMenu.DFMenu.GetValue().MenuDiches;
            _menuDishes.ForEach( dish => dish.Card = _dishCards.FirstOrDefault( card => card.Id == dish.CardId ) );
            _storeUpdated = new ManualResetEvent( false );
        }

        public new void Action()
        {
            _storeUpdated.WaitOne();
            Lock();
            for ( int i = 0; i < _menuDishes.Count; i++ )
            {
                bool actual = CheckDishActive( _menuDishes[ i ] );
                if (_menuDishes[i].Active != actual )
                {
                    _menuDishes[ i ].Active = actual;
                    _currentChanges.Add( _menuDishes[ i ] );
                }
            }
            if ( _visitorSsubscribers.Count > 0 )
            {
                foreach ( string visitorId in _visitorSsubscribers )
                {
                    SendMessageToAgent<VisitorRecieveMessage>( 
                        VisitorRecieveMessage.CreateVisitorUpdateMenuRequest( new VisitorMenuUpdatesMessage( _currentChanges ) ),
                        visitorId );
                }
            }
            _currentChanges.Clear();
            _loogger.LogInfo( "MenuAgent: menu updated" );
            _storeUpdated.Reset();
            Unlock();
        }

        public void UpdateProductsStore( Dictionary<int, double> updatedStore )
        {
            Lock();
            _storeUpdated.Reset();
            _store = updatedStore;
            _storeUpdated.Set();
            Unlock();
        }

        public List<MenuDish> GetActualMenu()
        {
            Lock();
            List<MenuDish> menuDishes = _menuDishes.ToList();
            Unlock();

            return menuDishes;
        }

        public void AddVisitorSubscriber( string visitorId )
        {
            Lock();
            _visitorSsubscribers.Add( visitorId );
            Unlock();
        }

        private bool CheckDishActive( MenuDish menuDish )
        {
            List<Prod> dishProducts = menuDish.Card.Operations.SelectMany( o => o.Products ).ToList();
            foreach ( Prod prod in dishProducts )
            {
                if ( _store[prod.Type] < prod.Quantity )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
