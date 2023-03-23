using IDZ3.Services.LogService;
using System.Text.Json;

namespace IDZ3.Services.LoadDataService
{
    public class FsLoadDataService : IFsLoadDataService
    {
        public FsLoadDataService( )
        {
        }

        public async Task<T> LoadDataAsync<T>( string filename )
        {
            try
            {
                string jsonDf = await File.ReadAllTextAsync( filename );
                T? deserializedData = JsonSerializer.Deserialize<T>( jsonDf );
                if ( deserializedData == null )
                {
                    throw new Exception( "data is null" );
                }

                return deserializedData;
            }
            catch ( Exception ex )
            {
                throw;
            }
        }

    }
}
