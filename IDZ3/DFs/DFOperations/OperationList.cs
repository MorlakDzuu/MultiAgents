using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFOperations
{
    public class OperationList
    {
        [JsonPropertyName( "operation_types" )]
        public List<OperationType> OperationTypes { get; set; }

        public OperationList( List<OperationType> operationTypes )
        {
            OperationTypes = operationTypes;
        }
    }
}
