using Microsoft.AspNetCore.Mvc;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;

namespace TrueDogStore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userPets = await _dashboardRepository.GetAllUserPets();
        
            return View(userPets);
        }
    }
}
