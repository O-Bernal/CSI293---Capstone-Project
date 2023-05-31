using Microsoft.AspNetCore.Mvc;

namespace MelodyRider_Back_End_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
