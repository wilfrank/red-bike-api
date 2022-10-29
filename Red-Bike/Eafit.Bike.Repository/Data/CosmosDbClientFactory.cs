using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.Bike.Repository.Data
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        private readonly string _databaseName;
        private readonly List<string> _collectionNames;
        private readonly IDocumentClient _documentClient;
        private readonly string _primaryKey;
        private readonly string _accountEndPoint;
        public CosmosDbClientFactory(string databaseName, List<string> collectionNames, IDocumentClient documentClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionNames = collectionNames ?? throw new ArgumentNullException(nameof(collectionNames));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }
        public CosmosDbClientFactory(string accountEndPoint, string authKey, string databaseName, List<string> collectionNames, IDocumentClient documentClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionNames = collectionNames ?? throw new ArgumentNullException(nameof(collectionNames));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
            _accountEndPoint = accountEndPoint ?? throw new ArgumentNullException(nameof(accountEndPoint));
            _primaryKey = authKey ?? throw new ArgumentNullException(nameof(authKey));
        }
        public ICosmosDbClient GetClient(string collectionName)
        {
            if (!_collectionNames.Contains(collectionName))
            {
                throw new ArgumentException($"Unable to find collection: {collectionName}");
            }

            //return new CosmosDbClient(_accountEndPoint, _primaryKey, _databaseName, collectionName, _documentClient);
            return new CosmosDbClient(_databaseName, collectionName, _documentClient);
        }
        public async Task EnsureDbSetupAsync()
        {
            try
            {
                await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseName));
            }
            catch (Exception)
            {
                var cosmosClient = new CosmosClient(_accountEndPoint, _primaryKey, new CosmosClientOptions() { ApplicationName = "bike-network" });
                var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
                foreach (var collectionName in _collectionNames)
                {
                    var container = await database.Database.CreateContainerIfNotExistsAsync(collectionName, "/Key", 400);
                    int? throughput = await container.Container.ReadThroughputAsync();
                    if (throughput.HasValue)
                    {
                        //Console.WriteLine("Current provisioned throughput : {0}\n", throughput.Value);
                        int newThroughput = throughput.Value + 100;
                        // Update throughput
                        await container.Container.ReplaceThroughputAsync(newThroughput);
                        //Console.WriteLine("New provisioned throughput : {0}\n", newThroughput);
                    }
                    //var databaseClient = GetClient(collectionName);
                    //await databaseClient.
                }
                //throw;
            }

            foreach (var collectionName in _collectionNames)
            {
                await _documentClient.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_databaseName, collectionName));
            }
        }

        public string GetDatabaseId()
        {
            return _databaseName;
        }
    }
}
