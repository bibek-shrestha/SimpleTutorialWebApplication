using Microsoft.AspNetCore.Mvc;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/cities/{cityid}/pointsofinterest")]
public class PointsOfInterestController: ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterests(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        return city == null ? NotFound() : Ok(city.PointOfInterests);
    }

    [HttpGet("{pointofinterestid}")]
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

}
