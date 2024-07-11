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
}
