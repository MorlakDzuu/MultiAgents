using IDZ3.Agents.Base;
using IDZ3.DFs.DFMenu;

namespace IDZ3.Agents.Menu
{
    public class MenuAgent : BaseAgent
    {
        private List<MenuDish> menuDishes;

        public MenuAgent( string ownerId ) : base( AgentRoles.MENU.ToString(), ownerId )
        {
            // loaded from file
            menuDishes = DFs.DFMenu.DFMenu.GetValue().MenuDiches;
        }

        public void Action()
        {

        }
    }
}
