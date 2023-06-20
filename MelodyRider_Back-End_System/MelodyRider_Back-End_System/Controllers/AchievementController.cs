using MelodyRider_Back_End_System.Data;
using MelodyRider_Back_End_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MelodyRider_Back_End_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AchievementController : Controller
    {
        // Inject services...
        private readonly GameDbContext _context;
        private readonly List<Achievement> achievements;

        public AchievementController(GameDbContext context, List<Achievement> achievements)
        {
            // Initialize services...
            _context = context;
            this.achievements = achievements;
        }

        public IActionResult Index()
        {
            // Get achievements...
            return View(achievements);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Achievement achievement)
        {
            // Validate and save the new achievement...
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            // Retrieve the achievement to be edited...
            return View(achievements);
        }

        [HttpPost]
        public IActionResult Edit(Achievement achievement)
        {
            // Validate and update the achievement...
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            // Retrieve the achievement to be deleted...
            return View(achievements);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete the achievement...
            return RedirectToAction("Index");
        }
    }

}
