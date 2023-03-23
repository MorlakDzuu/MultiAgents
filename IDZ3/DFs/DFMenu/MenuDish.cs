using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFMenu
{
    public class MenuDish
    {
        [JsonPropertyName( "menu_dish_id" )]
        public int Id { get; set; }
        
        [JsonPropertyName( "menu_dish_card" )]
        public int Card { get; set; }

        [JsonPropertyName( "menu_dish_price" )]
        public double Price { get; set; }

        [JsonPropertyName( "menu_dish_active" )]
        public bool Active { get; set; }

        public MenuDish( int id, int card, double price, bool active )
        {
            Id = id;
            Card = card;
            Price = price;
            Active = active;
        }
    }
}
