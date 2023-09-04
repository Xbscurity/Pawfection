using Microsoft.AspNetCore.Identity;
using System.Net;
using TrueDogStore.Models;

namespace TrueDogStore.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context?.Database.EnsureCreated();

                Pet pet1 = new Pet()
                {
                    Pet_Activity = new Pet_Activity()
                    {
                        Name = "Running",
                        Description = "Loves to go for long runs",
                        Activity_Level = new Activity_Level()
                        {
                            Name = "Low",
                            Description = "Requires minimal exercise",

                        }
                    },
                    Size = new Size()
                    {
                        Description = "Medium"
                    },
                    Breed = new Breed()
                    {
                        Name = "Golden Retriever",
                        Description = "Intelligent and friendly",
                        Breed_Group = new Breed_Group()
                        {
                            Name = "Sporting",
                            Description = "Dogs bred for hunting and retrieving"
                        }
                    },
                    Shelter = new Shelter()
                    {
                        Name = "Best Friends Shelter",
                        Address = "456 Elm Street",
                        Phone = "555-5678",
                        Email = "info@bestfriends.com"
                    },
                    Name = "Max",
                    ImagePath = "/images/golden-retriever-dog-breed-info.jpeg",
                    Fur_Length = "Medium",
                    Color = "Golden",
                    Weight = 30.2f,
                    Age = 2,
                    Gender = "Male",
                    Human_Friendly = true,
                    Pet_Friendly = true,
                    Apartment_Friendly = true,
                    Leash_Trained = true,
                    Litter_Trained = true
                };

                // Пример 2
                Pet pet2 = new Pet()
                {
                    Pet_Activity = new Pet_Activity()
                    {
                        Name = "Cuddling",
                        Description = "Enjoys snuggling on the couch",
                        Activity_Level = new Activity_Level()
                        {
                            Name = "High",
                            Description = "Requires a lot of exercise",

                        }

                    },
                    Size = new Size()
                    {
                        Description = "Small"
                    },
                    Breed = new Breed()
                    {
                        Name = "French Bulldog",
                        Description = "Affectionate and easygoing",
                        Breed_Group = new Breed_Group()
                        {
                            Name = "Non-Sporting",
                            Description = "Diverse group of breeds with different purposes"
                        }
                    },
                    Shelter = new Shelter()
                    {
                        Name = "Pawsome Shelter",
                        Address = "789 Oak Street",
                        Phone = "555-9012",
                        Email = "info@pawsome.com"
                    },
                    Name = "Bella",
                    ImagePath = "/images/french_bulldog.jpg",
                    Fur_Length = "Short",
                    Color = "Fawn",
                    Weight = 18.7f,
                    Age = 4,
                    Gender = "Female",
                    Human_Friendly = true,
                    Pet_Friendly = true,
                    Apartment_Friendly = true,
                    Leash_Trained = true,
                    Litter_Trained = true
                };
                Pet pet3 = new Pet()
                {
                    Pet_Activity = new Pet_Activity()
                    {
                        Name = "Playful",
                        Description = "Loves interactive playtime",
                        Activity_Level = new Activity_Level()
                        {
                            Name = "High",
                            Description = "Requires a lot of exercise",

                        }
                    },
                    Size = new Size()
                    {
                        Description = "Large"
                    },
                    Breed = new Breed()
                    {
                        Name = "German Shepherd",
                        Description = "Loyal and protective",
                        Breed_Group = new Breed_Group()
                        {
                            Name = "Herding",
                            Description = "Bred to herd livestock"
                        }
                    },
                    Shelter = new Shelter()
                    {
                        Name = "Rescue Haven",
                        Address = "987 Maple Street",
                        Phone = "555-3456",
                        Email = "info@rescuehaven.com"
                    },
                    Name = "Rocky",
                    ImagePath = "/images/german-shepherd-dog-breed-info.jpeg",
                    Fur_Length = "Medium",
                    Color = "Black and Tan",
                    Weight = 75.9f,
                    Age = 5,
                    Gender = "Male",
                    Human_Friendly = true,
                    Pet_Friendly = true,
                    Apartment_Friendly = false,
                    Leash_Trained = true,
                    Litter_Trained = false
                };
                Pet pet4 = new Pet()
                {
                    Pet_Activity = new Pet_Activity()
                    {
                        Name = "Grooming",
                        Description = "Requires regular grooming sessions",
                        Activity_Level = new Activity_Level()
                        {
                            Name = "Low",
                            Description = "Requires minimal exercise",

                        }
                    },
                    Size = new Size()
                    {
                        Description = "Small"
                    },
                    Breed = new Breed()
                    {
                        Name = "Poodle",
                        Description = "Intelligent and elegant",
                        Breed_Group = new Breed_Group()
                        {
                            Name = "Toy",
                            Description = "Small companion dogs"
                        }
                    },
                    Shelter = new Shelter()
                    {
                        Name = "Caring Paws Shelter",
                        Address = "321 Pine Street",
                        Phone = "555-7890",
                        Email = "info@caringpaws.com"
                    },
                    Name = "Lola",
                    ImagePath = "/images/toy-poodle.jpeg",
                    Fur_Length = "Long",
                    Color = "White",
                    Weight = 12.3f,
                    Age = 3,
                    Gender = "Female",
                    Human_Friendly = true,
                    Pet_Friendly = true,
                    Apartment_Friendly = true,
                    Leash_Trained = true,
                    Litter_Trained = true
                };
                context?.Pets.AddRange(new List<Pet>() { pet1, pet2, pet3, pet4 });
                context?.SaveChanges();
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "Obscuritydeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "obscurity",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}