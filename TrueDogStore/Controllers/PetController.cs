using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly ILogger<Pet> _logger;
        private readonly ApplicationDbContext _context;

        public PetController(IPetRepository petRepository, ILogger<Pet> logger, ApplicationDbContext context)
        {
            _petRepository = petRepository;
            _logger = logger;
            _context = context;
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
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        _logger.LogError($"Validation error for {key}: {error.ErrorMessage}");
                    }
                }
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
