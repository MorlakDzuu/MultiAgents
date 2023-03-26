using IDZ3.Agents.Visitor;

namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorActualMenuMessage
    {
        public List<VisitorMenuDish> Menu { get; set; }

        public VisitorActualMenuMessage( List<VisitorMenuDish> menu )
        {
            Menu = menu;
        }
    }
}
