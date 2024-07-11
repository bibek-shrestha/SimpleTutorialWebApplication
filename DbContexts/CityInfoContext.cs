using Microsoft.EntityFrameworkCore;
using SimpleTutorialWebApplication.Entities;

namespace SimpleTutorialWebApplication.DbContexts;

public class CityInfoContext: DbContext
{
    public DbSet<City> Cities { get; set; }

    public DbSet<PointOfInterest> PointOfInterests{ get; set; }

     public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City("Sydney")
            {
                Id = 1,
                Description = "Most popular city in the whole New South Wales."
            },
            new City("Burwood")
            {
                Id = 2,
                Description = "A major hub including china town."
            },
            new City("Rhodes")
            {
                Id = 3,
                Description = "A quite suburb with waterfronts to Parammatta River."
            }
        );
        modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest("Darling Harbor")
            {
                Id = 1,
                CityId = 1,
                Description = "A view of harbor illuminated with lights and other attractions."
            },
            new PointOfInterest("Barangaroo")
            {
                Id = 2,
                CityId = 1,
                Description = "A location for food and enjoyment with access to Ferries."
            },
            new PointOfInterest("China town")
            {
                Id = 3,
                CityId = 2,
                Description = "A collection of shops and entertainment from asian culture."
            },
            new PointOfInterest("Burwood Park")
            {
                Id = 4,
                CityId = 2,
                Description = "A green park to carry out some fitness routine and to get your body moving with some extra curricular activities."
            },
            new PointOfInterest("Water side")
            {
                Id = 5,
                CityId = 3,
                Description = "A great water side view of the Paramatta river."
            },
            new PointOfInterest("Mill Park")
            {
                Id = 6,
                CityId = 3,
                Description = "A park for casual workout and enjoy the Paramatta river."
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
