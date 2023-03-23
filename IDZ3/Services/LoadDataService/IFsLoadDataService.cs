namespace IDZ3.Services.LoadDataService
{
    public interface IFsLoadDataService
    {
        Task<T> LoadDataAsync<T>( string filename );
    }
}
