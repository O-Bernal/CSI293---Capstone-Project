using MelodyRider_Back_End_System.Data;
using MelodyRider_Back_End_System.Models;
using MelodyRider_Back_End_System.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelodyRider_Back_End_System.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GameDbContext _context;

        public UserController(UserManager<User> userManager, GameDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
		public async Task<IActionResult> Create(SignupViewModel model)
		{
			if (ModelState.IsValid)
            {
				var user = new User
				{
					UserName = model.Username,
					Email = model.Email,
					Role = "User" // Default role for new users
				};
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					// Return a JSON object with the username
					return Json(new { username = user.UserName });
				}
				else
				{
					return Json(new { error = result.Errors.Select(e => e.Description) });
				}
			}
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			return Json(new { error = errors });
		}

        // Other actions for Edit, Details, Delete...
    }
}
