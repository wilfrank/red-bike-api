using Eafit.Bike.Model;
using Eafit.Bike.Repository.ContextDb.Interfaces;
using Eafit.Bike.Repository.Data;
using Google.Cloud.Firestore;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace Eafit.Bike.Repository.ContextDb
{
    public abstract class FirestoreDbRepository<T> : IRepository<T>, IDocumentCollectionContext<T> where T : Entity
    {
        private readonly FirestoreDb _db;
        public abstract string CollectionName { get; }
        public FirestoreDbRepository(string projectId, string nameConfig = "eafit-arch-firebase.json")
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                $"{AppContext.BaseDirectory}{(nameConfig ?? "eafit-arch-firebase.json")}");
            _db = FirestoreDb.Create(projectId ?? "eafit-arch");
        }
        public async Task<T> AddAsync(T entity)
        {
            //throw new NotImplementedException();
            entity.Id = GenerateId(entity);
            var docRef = _db.Collection(typeof(T).Name).Document(entity.Id);
            await docRef.SetAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var docRef = _db.Collection(typeof(T).Name).Document(entity.ToString());
            await docRef.DeleteAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            //throw new NotImplementedException();
            var docRef = _db.Collection(typeof(T).Name).Document(id);
            var value = await docRef.GetSnapshotAsync();
            return value.ConvertTo<T>();
        }

        public FirestoreDb GetClient(string collectionName)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var docRef = _db.Collection(typeof(T).Name).Document(entity.Id);
            await docRef.SetAsync(entity).ConfigureAwait(false);
            return entity;
        }
        //public async PartitionKey ResolvePartitionKey(string entityId)
        //{
        //    var allData = _db.Collection(typeof(T).Name);
        //    var allDataQuery = await allData.GetSnapshotAsync();
        //    return allDataQuery.Documents.Select(d => d.ConvertTo<T>()).AsQueryable();
        //}

        public string GenerateId(T entity) => Guid.NewGuid().ToString();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var allData = _db.Collection(typeof(T).Name);
            var allDataQuery = await allData.GetSnapshotAsync();
            var allBikes = allDataQuery.Documents.Select(d => d.ConvertTo<T>()).AsQueryable();
            return allBikes;
        }

        public PartitionKey ResolvePartitionKey(string entityId)
        {
            throw new NotImplementedException();
        }
    }
}
