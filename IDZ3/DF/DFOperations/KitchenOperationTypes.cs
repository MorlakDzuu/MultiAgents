using System.Text.Json.Serialization;

namespace IDZ3.DF.DFOperations
{
    public class KitchenOperationTypes
    {
        [JsonPropertyName( "operation_types" )]
        public List<KithenOperationType> OperationTypes { get; set; }

        public KitchenOperationTypes( List<KithenOperationType> operationTypes )
        {
            OperationTypes = operationTypes;
        }
    }
}
