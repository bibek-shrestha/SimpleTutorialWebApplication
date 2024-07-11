using AutoMapper;
using SimpleTutorialWebApplication.Entities;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Profiles;

public class PointOfInterestProfile: Profile
{
    public PointOfInterestProfile()
    {
        CreateMap<PointOfInterest, PointOfInterestDto>();

        CreateMap<PointOfInterestCreationDto, PointOfInterest>();

        CreateMap<PointOfInterestUpdateDto, PointOfInterest>();

        CreateMap<PointOfInterest, PointOfInterestUpdateDto>();
    }
}
