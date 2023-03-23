using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFVisitors
{
    public class OrdDish
    {
        [JsonPropertyName( "ord_dish_id" )]
        public int DishId { get; set; }
        [JsonPropertyName( "menu_dish" )]
        public int MenuDish { get; set; }

        public OrdDish( int dishId, int menuDish )
        {
            DishId = dishId;
            MenuDish = menuDish; 
        }
    }
}
