namespace IDZ3.DFs.DFProductTypes
{
    public class DFProductTypes
    {
        private static ProductTypeList _productTypeList;

        public static void SetValue( ProductTypeList productTypeList )
        {
            _productTypeList = productTypeList;
        }

        public static ProductTypeList GetValue()
        {
            return _productTypeList;
        }
    }
}
