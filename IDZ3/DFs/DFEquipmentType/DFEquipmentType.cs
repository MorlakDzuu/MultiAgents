namespace IDZ3.DFs.DFEquipmentType
{
    public class DFEquipmentType
    {
        public static EquipmentTypeList _equipmentType;

        public static void SetValue( EquipmentTypeList equipmentType )
        {
            _equipmentType = equipmentType;
        }

        public static EquipmentTypeList GetValue()
        {
            return _equipmentType;
        }
    }
}
