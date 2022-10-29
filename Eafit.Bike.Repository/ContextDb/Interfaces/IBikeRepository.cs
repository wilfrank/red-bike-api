using Eafit.Bike.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.Bike.Repository.ContextDb.Interfaces
{
    public interface IBikeRepository : IRepository<BikeModel>
    {

        Task<BikeModel> Add(BikeModel bike);
        Task<IEnumerable<BikeModel>> GetAll();
    }
}
