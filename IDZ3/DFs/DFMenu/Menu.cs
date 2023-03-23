using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFMenu
{
    public class Menu
    {
        [JsonPropertyName( "menu_dishes" )]
        public List<MenuDish> MenuDiches { get; set; }

        public Menu( List<MenuDish> menuDiches )
        {
            MenuDiches = menuDiches;
        }
    }
}
