using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Eafit.Bike.Repository.Data
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IDocumentClient _documentClient;
        private readonly string _endPointUrl;
        private readonly string _authKey;

        public CosmosDbClient(string databaseName, string collectionName, IDocumentClient documentClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }
        //public CosmosDbClient(string endPointUrl, string authKey, string databaseName, string collectionName, IDocumentClient documentClient)
        //{
        //    _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        //    _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
        //    _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        //    _endPointUrl = endPointUrl ?? throw new ArgumentNullException(nameof(endPointUrl));
        //    _authKey = authKey ?? throw new ArgumentNullException(nameof(authKey));
        //}
        public async Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReadDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);
        }

        public async Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), document, options,
                disableAutomaticIdGeneration, cancellationToken);
        }

        public async Task<Document> ReplaceDocumentAsync(string documentId, object document,
            RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), document, options,
                cancellationToken);
        }

        public async Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);
        }

        public async Task<DocumentCollection> ReadAllDocumentAsync(RequestOptions options = null, CancellationToken cancellationToken = default)
        {
            //throw new NotImplementedException();
            var documents = await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName));
            return documents.Resource;
        }

        public IDocumentClient GetDocumentClient()
        {
            return _documentClient;
        }

        //public async Task<Database> GetDatabase()
        //{
        //    var cosmosClient = new CosmosClient(_endPointUrl, _authKey, new CosmosClientOptions() { ApplicationName = "bike-network" });
        //    var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        //    return database.Database;
        //}
    }
}
