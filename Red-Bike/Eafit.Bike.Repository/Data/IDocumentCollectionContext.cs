using Eafit.Bike.Model;
using Microsoft.Azure.Documents;

namespace Eafit.Bike.Repository.Data
{
    public interface IDocumentCollectionContext<in TEntity> where TEntity : Entity
    {
        string CollectionName { get; }
        string GenerateId(TEntity entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
