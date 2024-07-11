using SimpleTutorialWebApplication.Entities;

namespace SimpleTutorialWebApplication.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetAllCitiesAsync();

    Task<IEnumerable<City>> GetCitiesAsync(string? name);

    Task<City?> GetCityAsync(int cityId, bool includePointOfInterests);

    Task<bool> CityExistsAsync(int cityId);

    Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId);

    Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);

    Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

    Task<bool> SaveChangesAsync();

    void DeletePointOfInterest(PointOfInterest pointOfInterest);

}
