using Eafit.Bike.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.Bike.Repository.ContextDb.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        //Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
