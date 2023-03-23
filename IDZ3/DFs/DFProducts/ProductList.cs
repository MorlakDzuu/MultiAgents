using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFProducts
{
    public class ProductList
    {
        [JsonPropertyName( "products" )]
        public List<Product> ProductsList { get; set; }

        public ProductList( List<Product> productsList )
        {
            ProductsList = productsList;
        }
    }
}
