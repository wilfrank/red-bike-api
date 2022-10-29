//using Microsoft.Azure.Cosmos;
//using Microsoft.Azure.Documents;
//using System.Threading;
//using System.Threading.Tasks;
//using Database = Microsoft.Azure.Cosmos.Database;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Eafit.Bike.Repository.Data
{
    public interface ICosmosDbClient
    {
        IDocumentClient GetDocumentClient();
        Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null,
           CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<DocumentCollection> ReadAllDocumentAsync(RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken));
    }

    //Task<Document> ReadAllDocumentAsync(RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

    //Task<Database> GetDatabase();
}
