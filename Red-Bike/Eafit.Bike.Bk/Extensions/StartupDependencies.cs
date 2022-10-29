using Eafit.Bike.Repository.Data;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Eafit.Bike.Repository.ContextDb.Interfaces;
using Eafit.Bike.Repository.ContextDb;

namespace BIke_Network_bk.Extensions
{
    internal static class StartupDependencies
    {
        internal static void ConfigurationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IBikeRepository, BikeRepository>();
        }

        //internal static void AddCosmosDb(this IServiceCollection services, Uri serviceEndpoint,
        //    string authKey, string databaseName, List<string> collectionNames)
        //{
        //    var documentClient = new DocumentClient(serviceEndpoint, authKey, new JsonSerializerSettings
        //    {
        //        NullValueHandling = NullValueHandling.Ignore,
        //        DefaultValueHandling = DefaultValueHandling.Ignore,
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    });
        //    documentClient.OpenAsync().Wait();

        //    var cosmosDbClientFactory = new CosmosDbClientFactory(serviceEndpoint.ToString(), authKey, databaseName, collectionNames, documentClient);
        //    cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

        //    services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);
        //}

        internal static void AddFirestoreDb(this IServiceCollection services)
        {
            //var documentClient = new DocumentClient(serviceEndpoint, authKey, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    DefaultValueHandling = DefaultValueHandling.Ignore,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //});
            //documentClient.OpenAsync().Wait();

            //var firestoreDb = new FirestoreDbRepository(serviceEndpoint.ToString(), authKey, databaseName, collectionNames, documentClient);
            //cosmosDbClientFactory.EnsureDbSetupAsync().Wait();
       
            //var fireStore=new FirestoreDbRepository()

            //services.AddSingleton<F>(co => { new co()});
        }
    }
}
