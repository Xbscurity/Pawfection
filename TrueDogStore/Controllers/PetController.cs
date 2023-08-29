using Microsoft.AspNetCore.Mvc;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly ILogger<Pet> _logger;
        public PetController(IPetRepository petRepository, ILogger<Pet> logger)
        {
            _petRepository = petRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Pet> pets = await _petRepository.GetAll();
            return View(pets);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Pet pet = await _petRepository.GetByIdAsync(id);
            return View(pet);
        }
        public IActionResult Create()
        {
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
                return View(pet);
            }
            _petRepository.Add(pet);
            return RedirectToAction("Index");
        }
    }
}
