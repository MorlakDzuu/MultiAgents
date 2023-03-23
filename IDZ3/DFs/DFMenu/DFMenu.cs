namespace IDZ3.DFs.DFMenu
{
    public class DFMenu
    {
        private static Menu _menu;

        public static void SetValue( Menu menu )
        {
            _menu = menu;
        }

        public static Menu GetValue()
        {
            return _menu;
        }
    }
}
