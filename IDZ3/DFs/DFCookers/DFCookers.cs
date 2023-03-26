namespace IDZ3.DFs.DFCookers
{
    public class DFCookers
    {
        public static CookersList _cookers;

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
