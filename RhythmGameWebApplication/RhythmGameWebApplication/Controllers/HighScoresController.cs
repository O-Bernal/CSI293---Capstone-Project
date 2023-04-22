using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RhythmGameWebApplication.Data;
using RhythmGameWebApplication.Models;

namespace RhythmGameWebApplication.Controllers
{
    public class HighScoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HighScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action methods
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var highScores = await _context.HighScores.OrderByDescending(h => h.Score).Take(10).ToListAsync();
            return View(highScores);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitScore(HighScore newScore)
        {
            // Get the 100th highest score
            var minTopScore = await _context.HighScores.OrderByDescending(s => s.Score).Select(s => s.Score).Take(100).LastOrDefaultAsync();

            // Check if the new score qualifies for the top 100
            if (newScore.Score > minTopScore)
            {
                // The new score qualifies for the top 100, so add it to the database
            }
            else
            {
                // The new score doesn't qualify for the top 100, so return a message to store it on the client-side
                return Json(new { success = false, message = "Store the score on the client-side" });
            }

            // Add the new score
            _context.HighScores.Add(newScore);
            await _context.SaveChangesAsync();

            // Get the count of scores in the database
            var scoreCount = await _context.HighScores.CountAsync();

            // If the total number of scores is greater than 100, delete the lowest score
            if (scoreCount > 100)
            {
                var lowestScore = await _context.HighScores.OrderBy(s => s.Score).FirstOrDefaultAsync();
                _context.HighScores.Remove(lowestScore);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true, message = "The score has been added to the top 100" });

        }

    }

}
