using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodyRider_Back_End_System.Data;
using MelodyRider_Back_End_System.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace MelodyRider_Back_End_System.Controllers
{
    public class UserAchievementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GameDbContext _context;

        public UserAchievementController(UserManager<User> userManager, GameDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // POST: Creates the user's first achievement for hitting the start button on the Unity player.
        [HttpPost]
        public async Task<IActionResult> CreateWelcomeAchievement()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var welcomeAchievement = await _context.Achievements.FirstOrDefaultAsync(a => a.Name == "Welcome");
            if (welcomeAchievement == null)
            {
                // The "Welcome" achievement doesn't exist, so create it
                welcomeAchievement = new Achievement
                {
                    Name = "Welcome",
                    Description = "This is a welcome achievement for new players."
                };
                _context.Achievements.Add(welcomeAchievement);
                await _context.SaveChangesAsync();
            }

            var userAchievement = await _context.UserAchievements
                .FirstOrDefaultAsync(ua => ua.UserId == user.Id && ua.AchievementId == welcomeAchievement.AchievementId);

            if (userAchievement == null)
            {
                // The user doesn't have the "Welcome" achievement, so create it
                userAchievement = new UserAchievement
                {
                    UserId = user.Id,
                    AchievementId = welcomeAchievement.AchievementId,
                    DateEarned = DateTime.UtcNow
                };
                _context.UserAchievements.Add(userAchievement);
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true, username = user.UserName });
        }

        // GET: Gets all of the user's achievements
        [HttpGet]
        public async Task<IActionResult> GetUserAchievements()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userAchievements = _context.UserAchievements
                .Where(ua => ua.UserId == user.Id)
                .Include(ua => ua.Achievement)
                .ToList();

            return Ok(userAchievements);
        }
    }
}
