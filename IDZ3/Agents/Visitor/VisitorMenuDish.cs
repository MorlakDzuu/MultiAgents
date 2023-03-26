namespace IDZ3.Agents.Visitor
{
    public class VisitorMenuDish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Time { get; set; }
        public bool Active { get; set; }

        public VisitorMenuDish( int id, string name, double price, double time, bool active )
        {
            Id = id;
            Name = name;
            Price = price;
            Time = time;
            Active = active;
        }

        public override string ToString()
        {
            return $"***{Id}:: {Name}: price={Price}, time={Time}, active={Active}\n";
        }
    }
}
