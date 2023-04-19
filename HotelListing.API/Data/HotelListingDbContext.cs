using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        protected HotelListingDbContext()
        {

        }

        public HotelListingDbContext(DbContextOptions options) : base(options)
        {

        }


    }
}
