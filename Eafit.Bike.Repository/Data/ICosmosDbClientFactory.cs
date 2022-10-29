namespace Eafit.Bike.Repository.Data
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
        string GetDatabaseId();
    }
}
