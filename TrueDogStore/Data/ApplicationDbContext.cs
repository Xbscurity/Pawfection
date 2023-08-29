﻿using TrueDogStore.Models;
using Microsoft.EntityFrameworkCore;

namespace TrueDogStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Info> Users_Infos { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Breed_Group> BreedGroups { get; set; }
        public DbSet<Pet_Activity> PetActivities { get; set; }
        public DbSet<Activity_Level> ActivityLevels { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Shelter > Shelters { get; set; }


    }
}
