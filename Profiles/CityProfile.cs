using AutoMapper;
using SimpleTutorialWebApplication.Entities;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Profiles;

public class CityProfile: Profile
{
    public CityProfile()
    {
        CreateMap<City, CityWithoutPointOfInterestDto>();

        CreateMap<City, CityDto>();
    }

}
