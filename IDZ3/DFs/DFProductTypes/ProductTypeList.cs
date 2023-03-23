using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFProductTypes
{
    public class ProductTypeList
    {
        [JsonPropertyName( "product_types" )]
        public List<ProductType> ProductTypes { get; set; }

        public ProductTypeList( List<ProductType> productTypes )
        {
            ProductTypes = productTypes;
        }
    }
}
