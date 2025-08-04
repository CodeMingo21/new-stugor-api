using Microsoft.EntityFrameworkCore;
using StugorSe_API.Models;

namespace StugorAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet for each model
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Cottage> Cottages { get; set; }
        public DbSet<User> Users { get; set; }
        // Add other DbSets as needed
    }
    
}
