using Microsoft.EntityFrameworkCore;
using SimpleTutorialWebApplication.DbContexts;
using SimpleTutorialWebApplication.Entities;

namespace SimpleTutorialWebApplication.Services;

public class CityInfoRepository: ICityInfoRepository
{
    private readonly CityInfoContext _context;

    public CityInfoRepository(CityInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery) {
        var hasNoFilter = string.IsNullOrWhiteSpace(name);
        var hasNoSearchQuery = string.IsNullOrWhiteSpace(searchQuery);
        if (hasNoFilter && hasNoSearchQuery) return await GetAllCitiesAsync();
        var collection = _context.Cities as IQueryable<City>;
        if(!hasNoFilter)
        {
            name = name.Trim();
            collection = collection.Where(c => c.Name == name);
        }
        if(!hasNoSearchQuery)
        {
            searchQuery = searchQuery.Trim();
            collection = collection.Where(city => city.Name.Contains(searchQuery)
            || (city.Description!= null && city.Description.Contains(searchQuery)));
        }
        return await collection.OrderBy(city => city.Name).ToListAsync();
    }

    public async Task<bool> CityExistsAsync(int cityId) {
        return await _context.Cities.AnyAsync(c => c.Id == cityId);
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterests)
    {
        return includePointOfInterests ?
            await _context.Cities.Include(c => c.PointOfInterests).Where(c => c.Id == cityId).FirstOrDefaultAsync() :
            await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
    }

   public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
   {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
   }

   public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest) {
        var city = await GetCityAsync(cityId, false);
        city?.PointOfInterests.Add(pointOfInterest);
   }

   public async Task<bool> SaveChangesAsync()
   {
        return await _context.SaveChangesAsync() > 0;
   }

   public void DeletePointOfInterest(PointOfInterest pointOfInterest) {
        _context.PointOfInterests.Remove(pointOfInterest);
   }
}
