using Microsoft.AspNetCore.Mvc;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IShelterRepository _shelterRepository;
        private readonly ISearchRepository _searchRepository;
        public SearchController(IShelterRepository shelterRepository
            ,IPetRepository petRepository,ISearchRepository searchRepository)
        {
            _shelterRepository = shelterRepository;
            _petRepository = petRepository;
            _searchRepository = searchRepository;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(new List<Pet>());
            }
            var pets = await _searchRepository.SearchBreeds(search);
            return View(pets);
        }
    }
}
