namespace IDZ3.DFs.DFEquipment
{
    public class DFEquipment
    {
        private static EquipmentList _equipment;

        public static void SetValue( EquipmentList equipment )
        {
            _equipment = equipment;
        }

        public static EquipmentList GetValue()
        {
            return _equipment;
        }
    }
}
