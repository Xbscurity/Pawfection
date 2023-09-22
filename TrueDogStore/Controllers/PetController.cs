using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;
using TrueDogStore.Repository;
using TrueDogStore.ViewModels;

namespace TrueDogStore.Controllers
{
   
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly ILogger<Pet> _logger;
        private readonly IPhotoService _photoService;
        private readonly IShelterRepository _shelterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public PetController(IPetRepository petRepository, ILogger<Pet> logger,
            IPhotoService photoService, IShelterRepository ShelterRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _petRepository = petRepository;
            _logger = logger;         
            _photoService = photoService;
            _shelterRepository = ShelterRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Pet> pets = await _petRepository.GetAll();
            return View(pets);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Pet pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound(); 
            }
            return View(pet);
        }
        public async Task<IActionResult> Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();    
            var createPetViewModel = new CreatePetViewModel{ AppUserId =  curUserId };
            var shelters = await _shelterRepository.GetAll(); 
            ViewBag.Shelters = shelters;
            return View(createPetViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePetViewModel petVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(petVM.ImagePath);
                var shelter = await _shelterRepository.GetByIdAsync(petVM.Shelter.Id);

                if (shelter != null)
                {
                    
                    petVM.Shelter = shelter;
                }
                var pet = new Pet
                {
                    AppUserId = petVM.AppUserId,
                    Size = new Size
                    {                       
                    Description = petVM.Size.Description
                    },
                    Shelter = petVM.Shelter,
                    Pet_Activity = new Pet_Activity
                     {
                      Name = petVM.Pet_Activity.Name,
                      Description = petVM.Pet_Activity.Description,
                      Activity_Level = new Activity_Level
                          {
                             Name = petVM.Pet_Activity.Activity_Level.Name,
                             Description = petVM.Pet_Activity.Activity_Level.Description                 
                          }                    
                    },
                    Breed = new Breed
                    {
                    Name = petVM.Breed.Name,
                    Description = petVM.Breed.Description,
                    Breed_Group = new Breed_Group
                        {
                            Name = petVM.Breed.Breed_Group.Name,
                            Description = petVM.Breed.Breed_Group.Description
                        }   
                    
                    },
                    Name = petVM.Name,
                    Fur_Length = petVM.Fur_Length,
                    Color = petVM.Color,
                    Weight = petVM.Weight,
                    Age = petVM.Age,
                    Gender = petVM.Gender,
                    Apartment_Friendly = petVM.Apartment_Friendly,
                    Pet_Friendly = petVM.Pet_Friendly,
                    Leash_Trained = petVM.Leash_Trained,
                    Litter_Trained = petVM.Litter_Trained,
                    Human_Friendly = petVM.Human_Friendly,
                    ImagePath = result.Url.ToString()                   
                };
            _petRepository.Add(pet); 
            return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            
            }
            return View(petVM); 
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {              
                return NotFound(); 
            }
            var shelters = await _shelterRepository.GetAll();
			ViewBag.Shelters = shelters;
			var pet = await _petRepository.GetByIdAsync((int)id);
            if (pet == null)
            {
                return View("Error");
            }

            var petVM = new EditPetViewModel()
            {
                SizeId = pet.SizeId,
                Size = pet.Size,
                ShelterId = pet.ShelterId,
                Shelter = pet.Shelter,
                Pet_ActivityId = pet.Pet_ActivityId,
                Pet_Activity = pet.Pet_Activity,				
				BreedId = pet.BreedId,
                Breed = pet.Breed,		
                
                Name = pet.Name,
                Fur_Length = pet.Fur_Length,
                Color = pet.Color,
                Weight = pet.Weight,
                Age = pet.Age,
                Gender = pet.Gender,
                Apartment_Friendly = pet.Apartment_Friendly,
                Pet_Friendly = pet.Pet_Friendly,
                Leash_Trained = pet.Leash_Trained,
                Litter_Trained = pet.Litter_Trained,
                Human_Friendly = pet.Human_Friendly,
                URL = pet.ImagePath,                
            };
            return View(petVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPetViewModel petVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit pet");
                return View("Edit", petVM);
            }
            var shelter = await _shelterRepository.GetByIdAsyncNoTracking(petVM.Shelter.Id);
            var userPet = await _petRepository.GetByIdAsyncNoTracking(id);
            if (userPet != null)
            {
                try
                {
                    if (petVM.ImagePath != null)
                    {
                       
                        var fi = new FileInfo(userPet.ImagePath);
                        var publicId = Path.GetFileNameWithoutExtension(fi.Name);
                        await _photoService.DeletePhotoAsync(publicId);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Не удалось удалить фотографию");
                    return View(petVM);
                }

                string newImagePath = userPet.ImagePath;
                if (petVM.ImagePath != null)
                {
                    var photoResult = await _photoService.AddPhotoAsync(petVM.ImagePath);
                    newImagePath = photoResult.Url.ToString();
                }
                var pet = new Pet
                {
                    Id = id,
                    SizeId = petVM.SizeId,
                    Size = petVM.Size,
                    Shelter = new Shelter
                    {
                    Id = shelter.Id,
                    Address = shelter.Address,
                    Name = shelter.Name,
                    Email = shelter.Email,
                    Phone = shelter.Phone,
                    },
                    Pet_ActivityId = petVM.Pet_ActivityId,
                    Pet_Activity = petVM.Pet_Activity,
                    BreedId = petVM.BreedId,
                    Breed = petVM.Breed,
                    Name = petVM.Name,
                    Fur_Length = petVM.Fur_Length,
                    Color = petVM.Color,
                    Weight = petVM.Weight,
                    Age = petVM.Age,
                    Gender = petVM.Gender,
                    Apartment_Friendly = petVM.Apartment_Friendly,
                    Pet_Friendly = petVM.Pet_Friendly,
                    Leash_Trained = petVM.Leash_Trained,
                    Litter_Trained = petVM.Litter_Trained,
                    Human_Friendly = petVM.Human_Friendly,
                    ImagePath = newImagePath,

                };
                _petRepository.Update(pet);
                return RedirectToAction("Index");
            }
            else
            {
                return View(petVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
 
            var petDetails = await _petRepository.GetByIdAsync(id);
            if (petDetails == null) return View("Error");
            return View(petDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var petDetails = await _petRepository.GetByIdAsync(id);
            var fi = new FileInfo(petDetails.ImagePath);
            var publicId = Path.GetFileNameWithoutExtension(fi.Name);
            await _photoService.DeletePhotoAsync(publicId);
            if (petDetails == null) return View("Error");
            _petRepository.Delete(petDetails);
            return RedirectToAction("Index");
        }
    }
}
