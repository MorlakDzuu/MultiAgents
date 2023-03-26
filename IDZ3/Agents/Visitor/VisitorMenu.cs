namespace IDZ3.Agents.Visitor
{
    public class VisitorMenu
    {
        public List<VisitorMenuDish> Dishes { get; set; }

        public VisitorMenu( List<VisitorMenuDish> dishes )
        {
            Dishes = dishes;
        }

        public override string ToString()
        {
            string menuItems = String.Empty;
            Dishes.ForEach( d => menuItems += d.ToString() );
            return $"\nMenu:\n{menuItems}";
        }
    }
}
