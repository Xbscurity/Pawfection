using Microsoft.AspNetCore.Mvc;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;
using TrueDogStore.ViewModels;

namespace TrueDogStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(new List<Pet>());
            }
            var pets = await _searchRepository.SearchBreeds(search);
            var searchVM = new List<SearchResultViewModel>();
            foreach (var pet in pets)
            {
                searchVM.Add(new SearchResultViewModel
                {
                    Id = pet.Id,
                    Shelter = pet.Shelter,
                    ImagePath = pet.ImagePath,
                    Breed = pet.Breed,
                });
            }
            return View(searchVM);
        }
    }
}
