using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.AddControllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController: ControllerBase {

        private readonly CitiesDataStore _cityDataStore;

        public CityController(CitiesDataStore cityDataStore)
        {
            _cityDataStore = cityDataStore ?? throw new ArgumentNullException(nameof(cityDataStore));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities() {
            return Ok(_cityDataStore.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id) {
            var city = _cityDataStore.Cities.FirstOrDefault(city => city.Id == id);
            return city == null ? NotFound() : Ok(city);
        }

    }
}