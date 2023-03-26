using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFDishCards
{
    public class DishCard
    {
        [JsonPropertyName( "card_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "dish_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "card_descr" )]
        public string Description { get; set; }

        [JsonPropertyName( "card_time" )]
        public double Time { get; set; }

        [JsonPropertyName( "operations" )]
        public List<Operation> Operations { get; set; }

        public DishCard( int id, string name, string description, double time, List<Operation> operations )
        {
            Id = id;
            Name = name;
            Description = description;
            Time = time;
            Operations = operations;
        }
    }
}
