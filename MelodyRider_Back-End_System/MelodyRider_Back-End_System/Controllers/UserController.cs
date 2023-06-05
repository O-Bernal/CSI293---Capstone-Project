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
        private readonly GameDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(GameDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // POST: Users/Create (Signup)
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
                    await _signInManager.SignInAsync(user, isPersistent: false);
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

        // POST: Users/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user;
                if (model.UsernameOrEmail.Contains("@"))
                {
                    // The input looks like an email address
                    user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
                }
                else
                {
                    // The input is probably a username
                    user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
                }

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        // Return a JSON object indicating success
                        return Json(new { success = true });
                    }
                }

                // Return a JSON object with an error message
                return Json(new { error = new[] { "Invalid login attempt." } });
            }

            // Return a JSON object with model state errors
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return Json(new { error = errors });
        }

    }

}
