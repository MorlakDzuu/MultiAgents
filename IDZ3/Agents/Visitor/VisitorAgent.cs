using IDZ3.Agents.Base;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Visitor;
using IDZ3.MessagesContracts;

namespace IDZ3.Agents.Visitor
{
    /// <summary>
    /// Агент посетителя
    /// </summary>
    public class VisitorAgent : BaseAgent
    {
        // Id управляющего агента
        private string _adminAgentId;
        private List<VisitorMenuDish> _menuDishes;

        private List<int> _choosenDisches;
        private ManualResetEvent _manualReset;
        private VisitorAgentStatus _visitorStatus;

        public VisitorAgent( string ownerId, string adminId ) : base( AgentRoles.VISITOR.ToString(), ownerId )
        {
            _adminAgentId = adminId;
            _menuDishes = new List<VisitorMenuDish>();
            _choosenDisches = new List<int>();
            _manualReset = new ManualResetEvent( false );

            _visitorStatus = VisitorAgentStatus.INIT;
        }
        //private List<MenuDish> menuDishes;

        public new void Action()
        {
            _manualReset.Reset();
            Lock();
            switch ( _visitorStatus )
            {
                case VisitorAgentStatus.INIT:
                    SendMessageToAgent<AdminMessage>(
                    AdminMessage.VisitorCreateAdminMassege( VisitorMessage.CreateMenuRequestMessage( 20 ) ), _adminAgentId );
                    _menuDishes = GetMessage<List<VisitorMenuDish>>().MessageContent;

                    _loogger.LogInfo( $"Visitor {Id}: got actual menu: {( new VisitorMenu( _menuDishes ) ).ToString()}" );
                    break;
                case VisitorAgentStatus.ASK_ADMIN_TO_CREATE_ORDER:
                    CreateOrder();
                    Message<string> orderAgentId = GetMessage<string>();
                    _loogger.LogInfo( $"Visitor got an order agent {orderAgentId.MessageContent}" );
                    break;
            }
            Unlock();
            _manualReset.WaitOne();
        }

        public void AddDishToOrder( int menuDishId )
        {
            Lock();
            _choosenDisches.Add( menuDishId );
            _loogger.LogInfo( $"Visitor {Id}: add {menuDishId} to order" );
            Unlock();
        }

        public void RemoveDishFromOrder( int menuDishId )
        {
            Lock();
            if ( _choosenDisches.Contains( menuDishId ) )
            {
                _choosenDisches.Remove( menuDishId );
                _loogger.LogInfo( $"Visitor {Id}: remove {menuDishId} from order" );
            }
            Unlock();
        }

        public void MakeOrder()
        {
            Lock();
            _visitorStatus = VisitorAgentStatus.ASK_ADMIN_TO_CREATE_ORDER;
            Unlock();
            _manualReset.Set();
        }

        private void CreateOrder()
        {
            if ( _choosenDisches.Count == 0 )
            {
                return;
            }
            VisitorMessage visitorMessage = VisitorMessage.CreateMakeOrderMessage( _choosenDisches );
            SendMessageToAgent<AdminMessage>(
                AdminMessage.VisitorCreateAdminMassege( visitorMessage ) , _adminAgentId );
        }
    }
}
