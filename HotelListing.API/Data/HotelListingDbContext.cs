using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace HotelListing.API.Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasData(

            new Country
            {
                Id = 1,
                Name = "Jamaica",
                ShortName = "JM"
            },
            new Country
            {
                Id = 2,
                Name = "Bahamas",
                ShortName = "BS"
            },
            new Country
            {
                Id = 3,
                Name = "Cayman Island",
                ShortName = "CI"
            }
        );


        modelBuilder.Entity<Hotel>().HasData(

            new Hotel
            {
                Id = 1,
                Name = "Sandals Resort and Spa",
                Address = "Negril",
                CountryId = 1,
                Rating = 4.5
            },
            new Hotel
            {
                Id = 2,
                Name = "Comfort Suites",
                Address = "George Town",
                CountryId = 3,
                Rating = 4.3
            },
            new Hotel
            {
                Id = 3,
                Name = "Grand Palladium",
                Address = "Nassua",
                CountryId = 2,
                Rating = 4
            }
        );

    }

    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Country> Countries { get; set; }
}