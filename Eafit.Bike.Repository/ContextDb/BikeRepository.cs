using Eafit.Bike.Model;
using Eafit.Bike.Repository.ContextDb.Interfaces;
using Eafit.Bike.Repository.Data;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace Eafit.Bike.Repository.ContextDb
{
    public class BikeRepository : FirestoreDbRepository<BikeModel>, IBikeRepository//, CosmosDbRepository<BikeModel>
    {
        //private readonly ICosmosDbClientFactory _cosmosDbClient;
        private readonly IFirestoreDbClientFactory _firestoreDbClient;
        public BikeRepository() : base("eafit-arch")
        {
            //_cosmosDbClient = cosmosDbClientFactory;
            //_firestoreDbClient = firestoreDbClientFactory;
        }
        public override string CollectionName { get; } = "bikesItems";

        public async Task<BikeModel> Add(BikeModel bike)
        {
            var firestoreDb = _firestoreDbClient.GetFirestoreDb();
            //firestoreDb.
            //_firestoreDbClient.
            var docRef = firestoreDb.Collection(bike.GetType().Name).Document(bike.ToString());
            await docRef.SetAsync(bike).ConfigureAwait(false);
            return bike;
        }

        //public override string GenerateId(BikeModel entity) => $"{entity.Model}:{Guid.NewGuid()}";

        public async Task<IEnumerable<BikeModel>> GetAll()
        {
            //var dbClient = _cosmosDbClient.GetClient(CollectionName);
            var _db = _firestoreDbClient.GetFirestoreDb();
            //var docQuery = dbClient.GetDocumentClient();
            var allData = _db.Collection(typeof(BikeModel).Name);
            var allDataQuery = await allData.GetSnapshotAsync();
            var data = allDataQuery.Documents.Select(d => d.ConvertTo<BikeModel>()).AsQueryable();
            //var bikes = docQuery.CreateDocumentQuery<BikeModel>(UriFactory.CreateDocumentCollectionUri(_cosmosDbClient.GetDatabaseId()
            //, CollectionName));//.Where(bk => bk.IsActived == true);
            //return bikes.ToList();
            return data;
        }

        //public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

    }
}
