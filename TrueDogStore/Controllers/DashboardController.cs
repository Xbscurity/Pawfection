using Microsoft.AspNetCore.Mvc;

namespace TrueDogStore.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
