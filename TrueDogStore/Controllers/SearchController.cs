using Microsoft.AspNetCore.Mvc;

namespace TrueDogStore.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
