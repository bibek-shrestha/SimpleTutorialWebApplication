using SimpleTutorialWebApplication.Entities;

namespace SimpleTutorialWebApplication.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetAllCitiesAsync();

    Task<City?> GetCityAsync(int cityId, bool includePointOfInterests);

    Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId);

    Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);

}
