using MelodyRider_Back_End_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelodyRider_Back_End_System.Controllers
{
    public class ScoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
