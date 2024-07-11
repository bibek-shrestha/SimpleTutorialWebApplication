using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Models;
using SimpleTutorialWebApplication.Services;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/cities/{cityid}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;

    private readonly IMailService _mailService;
    private readonly ICityInfoRepository _cityInfoRepository;

    private readonly IMapper _mapper;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger
            , IMailService mailService
            , ICityInfoRepository cityInfoRepository
            , IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterests(int cityId)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var pointOfInterests =  await _cityInfoRepository.GetPointOfInterestsForCityAsync(cityId);
        return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterests));

    }

    [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
    public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var pointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if(pointOfInterest == null) return NotFound();
        return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
    }

    // [HttpPost]
    // public ActionResult<PointOfInterestDto> AddPointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
    // {
    //     var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);
    //     if (city == null)
    //     {
    //         return NotFound();
    //     }
    //     var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(city => city.PointOfInterests).Max(p => p.Id);
    //     var finalPointOfInterest = new PointOfInterestDto()
    //     {
    //         Id = ++maxPointOfInterestId,
    //         Name = pointOfInterestCreationDto.Name,
    //         Description = pointOfInterestCreationDto.Description,
    //     };
    //     city.PointOfInterests.Add(finalPointOfInterest);
    //     return CreatedAtRoute("GetPointOfInterest", new
    //     {
    //         cityId = city.Id,
    //         pointOfInterestId = finalPointOfInterest.Id
    //     }, finalPointOfInterest);
    // }

    // [HttpPut("{pointofinterestid}")]
    // public ActionResult<PointOfInterestDto> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdateDto)
    // {
    //     var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);
    //     if (city == null)
    //     { return NotFound(); }
    //     var pointOfInterest = city.PointOfInterests.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
    //     if (pointOfInterest == null)
    //     {
    //         return NotFound();
    //     }
    //     pointOfInterest.Name = pointOfInterestUpdateDto.Name;
    //     pointOfInterest.Description = pointOfInterestUpdateDto.Description;
    //     return NoContent();
    // }

    // [HttpPatch("{pointofinterestid}")]
    // public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId
    //         , JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
    // {
    //     var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);
    //     if (city == null)
    //     {
    //         return NotFound();
    //     }
    //     var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
    //     if (pointOfInterestFromStore == null)
    //     {
    //         return NotFound();
    //     }
    //     var pointOfInterestUpdateDto = new PointOfInterestUpdateDto() {
    //         Name = pointOfInterestFromStore.Name,
    //         Description = pointOfInterestFromStore.Description
    //     };
    //     patchDocument.ApplyTo(pointOfInterestUpdateDto, ModelState);
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     if (!TryValidateModel(pointOfInterestUpdateDto))
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     pointOfInterestFromStore.Name = pointOfInterestUpdateDto.Name;
    //     pointOfInterestFromStore.Description = pointOfInterestUpdateDto.Description;
    //     return NoContent();
    // }

    // [HttpDelete("{pointofinterestid}")]
    // public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    // {
    //     var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);
    //     if (city == null)
    //     {
    //         return NotFound();
    //     }
    //     var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
    //     if (pointOfInterestFromStore == null)
    //     {
    //         return NotFound();
    //     }
    //     city.PointOfInterests.Remove(pointOfInterestFromStore);
    //     _mailService.Send("test", "test");
    //     return NoContent();
    // }

}
