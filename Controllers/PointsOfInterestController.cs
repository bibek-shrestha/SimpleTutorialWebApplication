using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Entities;
using SimpleTutorialWebApplication.Models;
using SimpleTutorialWebApplication.Services;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/cities/{cityid}/pointsofinterest")]
[Authorize]
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

    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>> AddPointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestCreationDto);
        await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);
        await _cityInfoRepository.SaveChangesAsync();
        var createdPointOfInterest = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);
        return CreatedAtRoute("GetPointOfInterest", new
        {
            cityId = cityId,
            pointOfInterestId = createdPointOfInterest.Id
        }, createdPointOfInterest);
    }

    [HttpPut("{pointofinterestid}")]
    public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdateDto)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var pointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterest == null) return NotFound();
        _mapper.Map(pointOfInterestUpdateDto, pointOfInterest);
        await _cityInfoRepository.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{pointofinterestid}")]
    public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId
            , JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var pointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterest == null) return NotFound();
        var pointOfInterestUpdateDto = _mapper.Map<PointOfInterestUpdateDto>(pointOfInterest);
        patchDocument.ApplyTo(pointOfInterestUpdateDto, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!TryValidateModel(pointOfInterestUpdateDto))
        {
            return BadRequest(ModelState);
        }
        _mapper.Map(pointOfInterestUpdateDto, pointOfInterest);
        await _cityInfoRepository.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{pointofinterestid}")]
    public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        if(!await _cityInfoRepository.CityExistsAsync(cityId)) return NotFound();
        var pointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterest == null) return NotFound();
        _cityInfoRepository.DeletePointOfInterest(pointOfInterest);
        await _cityInfoRepository.SaveChangesAsync();
        _mailService.Send("test", "test");
        return NoContent();
    }

}
