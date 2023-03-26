using IDZ3.DFs.DFDishCards;
using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFMenu
{
    public class MenuDish
    {
        [JsonPropertyName( "menu_dish_id" )]
        public int Id { get; set; }
        
        [JsonPropertyName( "menu_dish_card" )]
        public int CardId { get; set; }

        [JsonPropertyName( "menu_dish_price" )]
        public double Price { get; set; }

        [JsonPropertyName( "menu_dish_active" )]
        public bool Active { get; set; }

        [JsonIgnore]
        public DishCard? Card { get; set; }

        public MenuDish( int id, int cardId, double price, bool active )
        {
            Id = id;
            CardId = cardId;
            Price = price;
            Active = active;
        }
    }
}
