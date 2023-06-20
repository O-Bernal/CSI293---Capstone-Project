using MelodyRider_Back_End_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelodyRider_Back_End_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Inject services...
        private readonly GameDbContext _context;

        public AdminController(GameDbContext context)
        {
            // Initialize services...
            _context = context;
        }

        public IActionResult ManageScores()
        {
            // Get scores...
            return View();
        }

        public IActionResult ManageAchievements()
        {
            // Get achievements...
            var achievements = _context.UserAchievements.Include(ua => ua.Achievement).ToList();

            return View(achievements);
        }

        public IActionResult ManageUsers()
        {
            // Get users...
            return View();
        }

        // Other admin actions...
    }
}
