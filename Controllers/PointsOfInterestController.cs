using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/cities/{cityid}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterests(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        return city == null ? NotFound() : Ok(city.PointOfInterests);
    }

    [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterests(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterest = city.PointOfInterests.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        return pointOfInterest == null ? NotFound() : Ok(pointOfInterest);
    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> addPointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(city => city.PointOfInterests).Max(p => p.Id);
        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterestCreationDto.Name,
            Description = pointOfInterestCreationDto.Description,
        };
        city.PointOfInterests.Add(finalPointOfInterest);
        return CreatedAtRoute("GetPointOfInterest", new
        {
            cityId = city.Id,
            pointOfInterestId = finalPointOfInterest.Id
        }, finalPointOfInterest);
    }

}
