using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;
using TrueDogStore.ViewModels;

namespace TrueDogStore.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly ILogger<Pet> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public PetController(IPetRepository petRepository, ILogger<Pet> logger, ApplicationDbContext context, IPhotoService photoService)
        {
            _petRepository = petRepository;
            _logger = logger;
            _context = context;
            _photoService = photoService;
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
        public IActionResult Create()
        {
            var shelters = _context.Shelters.ToList(); 
            ViewBag.Shelters = shelters;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
              // var result = await _photoService.AddPhotoAsync(pet.ImagePath)
                var shelter = _context.Shelters.FirstOrDefault(s => s.Name == pet.Shelter.Name);

                if (shelter != null)
                {
                    pet.ShelterId = shelter.Id;
                    _context.Add(pet);
                    await _context.SaveChangesAsync();
                }
                return View(pet);
            }
            _petRepository.Add(pet);
            return RedirectToAction("Index");
        }
    }
}
