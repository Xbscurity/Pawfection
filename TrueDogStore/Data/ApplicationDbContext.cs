using TrueDogStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TrueDogStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Breed_Group> BreedGroups { get; set; }
        public DbSet<Pet_Activity> PetActivities { get; set; }
        public DbSet<Activity_Level> ActivityLevels { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Shelter > Shelters { get; set; }


    }
}
