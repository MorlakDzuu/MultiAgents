namespace IDZ3.DFs.DFProducts
{
    public class DFProducts
    {
        private static ProductList _products;

        public static void SetValue( ProductList products )
        {
            _products = products;
        }

        public static ProductList GetValue()
        {
            return _products;
        }
    }
}
