using Eafit.Bike.Model;
using Eafit.Bike.Repository.ContextDb.Interfaces;
using Eafit.Bike.Repository.Data;
using Eafit.Bike.Repository.Exceptions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Eafit.Bike.Repository.ContextDb
{
    public abstract class CosmosDbRepository<TEntity> : IRepository<TEntity>, IDocumentCollectionContext<TEntity> where TEntity : Entity
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;
        protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
        }

        public abstract string CollectionName { get; }

        public async Task<TEntity>? AddAsync(TEntity entity)
        {
            try
            {
                entity.Id = GenerateId(entity);
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.CreateDocumentAsync(entity);
                return JsonConvert.DeserializeObject<TEntity>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException();
                }

                throw;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.DeleteDocumentAsync(entity.Id);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }
        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentAsync(id);

                return JsonConvert.DeserializeObject<TEntity>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public virtual PartitionKey ResolvePartitionKey(string entityId) => null;

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.ReplaceDocumentAsync(entity.Id, entity);
                return entity;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public virtual string GenerateId(TEntity entity) => Guid.NewGuid().ToString();

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
