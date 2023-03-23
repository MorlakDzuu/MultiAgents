namespace IDZ3.DFs.DFVisitors
{
    public class DFVisitors
    {
        private static VisitorOrderList _visitorOrderList;

        public static void SetValue( VisitorOrderList visitorOrderList )
        {
            _visitorOrderList = visitorOrderList;
        }

        public static VisitorOrderList GetValue()
        {
            return _visitorOrderList;
        }
    }
}
