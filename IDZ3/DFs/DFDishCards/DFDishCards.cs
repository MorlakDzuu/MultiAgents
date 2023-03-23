namespace IDZ3.DFs.DFDishCards
{
    public class DFDishCards
    {
        private static DishCardList _dishCards;

        public static void SetValue( DishCardList dishCardList )
        {
            _dishCards = dishCardList;
        }

        public static DishCardList GetValue()
        {
            return _dishCards;
        }
    }
}
