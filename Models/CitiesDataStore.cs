using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Models;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto() {
                Id = 1,
                Name = "Sydney",
                Description = "Most popular city in the whole New South Wales.",
                PointOfInterests = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto() 
                    {
                        Id = 1,
                        Name = "Darling Harbor",
                        Description = "A view of harbor illuminated with lights and other attractions."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Barangaroo",
                        Description = "A location for food and enjoyment with access to Ferries."
                    }
                }
            },
            new CityDto() {
                Id = 2,
                Name = "Burwood",
                Description = "A major hub including china town.",
                PointOfInterests = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        Name = "China town",
                        Description = "A collection of shops and entertainment from asian culture."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Burwood Park",
                        Description = "A green park to carry out some fitness routine and to get your body moving with some extra curricular activities."
                    }
                }
            },
            new CityDto() {
                Id = 3,
                Name = "Rhodes",
                Description = "A quite suburb with waterfronts to Parammatta River.",
                PointOfInterests = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 5,
                        Name = "Water side",
                        Description = "A great water side view of the Paramatta river."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        Name = "Mill Park",
                        Description = "A park for casual workout and enjoy the Paramatta river."

                    }
                }
            }
        };
    }
}
