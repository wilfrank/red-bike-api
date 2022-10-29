using Eafit.Bike.Model;
using Eafit.Bike.Repository.ContextDb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BIke_Network_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBikeRepository _bikeRepository;
        public BikeController(ILogger<WeatherForecastController> logger, IBikeRepository bikeRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository ?? throw new ArgumentNullException(nameof(bikeRepository)); ;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<BikeModel>> Get()
        {
            _logger.Log(LogLevel.Information, "Getting bike data");
            var bikes = await _bikeRepository.GetAllAsync();
            bikes = bikes.Where(b => b.IsActived == true);
            return bikes;
        }
        [HttpPost]
        [Route("New")]
        public async Task<BikeModel> Create(BikeModel bike)
        {
            _logger.Log(LogLevel.Information, "Getting bike data");
            var newBike = await _bikeRepository.AddAsync(bike);
            return newBike;
        }
        [HttpPut]
        [Route("Update")]
        public async Task<BikeModel> Update(BikeModel bike)
        {
            _logger.Log(LogLevel.Information, "Getting bike data");
            var newBike = await _bikeRepository.AddAsync(bike);
            return newBike;
        }
        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<BikeModel> Delete(string key)
        {
            _logger.Log(LogLevel.Information, "Getting bike data");
            var bike = await _bikeRepository.GetByIdAsync(key);
            bike.IsActived = false;
            await _bikeRepository.UpdateAsync(bike);
            return bike;
        }
    }
}
