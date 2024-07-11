using SimpleTutorialWebApplication.Entities;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetAllCitiesAsync();

    Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

    Task<City?> GetCityAsync(int cityId, bool includePointOfInterests);

    Task<bool> CityExistsAsync(int cityId);

    Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId);

    Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);

    Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

    Task<bool> SaveChangesAsync();

    void DeletePointOfInterest(PointOfInterest pointOfInterest);

}
