using System.Text.Json;
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

        private const int MAX_PAGE_SIZE = 20;

        public CityController(ICityInfoRepository cityInfoRepository
                , IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities(
            [FromQuery(Name = "name_filter")] string? cityName
            , [FromQuery(Name = "search_query")] string? searchQuery
            , [FromQuery(Name = "page_number")] int pageNumber = 1
            , [FromQuery(Name = "page_size")] int pageSize = 10
        ) {
            if (pageSize > MAX_PAGE_SIZE) pageSize = MAX_PAGE_SIZE;
            var (cities, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(cityName, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, [FromQuery(Name = "include_point_of_interest")] bool includePointOfInterests = false) {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterests);
            return city == null
                ? NotFound()
                : (includePointOfInterests 
                    ? Ok(_mapper.Map<CityDto>(city))
                    : Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city)));
        }

    }
}