namespace IDZ3.DFs.DFCookers
{
    public class DFCookers
    {
        private static CookersList _cookers;

        public static void SetValue( CookersList cookers )
        {
            _cookers = cookers;
        }

        public static CookersList GetValue()
        {
            return _cookers;
        }
    }
}
