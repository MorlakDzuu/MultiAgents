using IDZ3.DFs.DFMenu;

namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorMenuUpdatesMessage
    {
        public List<MenuDish> UpdatedDishes { get; set; }

        public VisitorMenuUpdatesMessage( List<MenuDish> updatedDishes )
        {
            UpdatedDishes = updatedDishes;
        }
    }
}
