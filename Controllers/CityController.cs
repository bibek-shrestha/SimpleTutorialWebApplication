using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Models;
using SimpleTutorialWebApplication.Services;

namespace SimpleTutorialWebApplication.AddControllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController: ControllerBase {

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CityController(ICityInfoRepository cityInfoRepository
                , IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities() {
            var cities = await _cityInfoRepository.GetAllCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterests = false) {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterests);
            return city == null
                ? NotFound()
                : (includePointOfInterests 
                    ? Ok(_mapper.Map<CityDto>(city))
                    : Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city)));
        }

    }
}