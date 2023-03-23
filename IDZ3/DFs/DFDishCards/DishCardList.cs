using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFDishCards
{
    public class DishCardList
    {
        [JsonPropertyName( "dish_cards" )]
        public List<DishCard> DishCards { get; set; }

        public DishCardList( List<DishCard> dishCards )
        {
            DishCards = dishCards;
        }
    }
}
