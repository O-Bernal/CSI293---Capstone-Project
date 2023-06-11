using MelodyRider_Back_End_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelodyRider_Back_End_System.Controllers
{
    public class GameController : Controller
    {
        private readonly GameDbContext _context;

        public GameController(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var topScores = await _context.Scores
                .Include(s => s.User)
                .OrderByDescending(s => s.ScoreValue)
                .Take(10)
                .ToListAsync();

            return View(topScores);
        }

    }
}
