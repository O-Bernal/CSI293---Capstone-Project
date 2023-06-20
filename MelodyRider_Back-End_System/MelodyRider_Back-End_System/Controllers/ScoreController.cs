using MelodyRider_Back_End_System.Data;
using MelodyRider_Back_End_System.Models;
using MelodyRider_Back_End_System.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MelodyRider_Back_End_System.Controllers
{
    public class ScoreController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GameDbContext _context;

        public ScoreController(UserManager<User> userManager, GameDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> UserScores()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var scores = _context.Scores
                .Where(s => s.UserId == user.Id).ToList();

            var model = new UserSettingsViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return View("Settings", (model, user, scores));
        }
    }
}
