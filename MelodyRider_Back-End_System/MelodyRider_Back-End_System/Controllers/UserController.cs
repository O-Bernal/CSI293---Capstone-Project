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
        private readonly ILogger<UserController> _logger;

        public UserController(GameDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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
                    // Check if the user is deleted
                    if (user.IsDeleted)
                    {
                        // Return a JSON object with an error message
                        return Json(new { error = new[] { "Invalid login attempt." } });
                    }

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

        // POST: Users/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Game");
        }

        // GET: Redirect to the User's settings page
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var scores = _context.Scores.Where(s => s.UserId == user.Id).ToList();
            var userAchievements = _context.UserAchievements
                .Include(ua => ua.Achievement) // This line ensures that the Achievement entities are loaded
                .Where(ua => ua.UserId == user.Id)
                .ToList();

            var model = new UserSettingsViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                User = user,
                Scores = scores,
                UserAchievements = userAchievements
            };

            return View(model);
        }

        /* Old Settings Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(UserSettingsViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            // Checks if the user is trying to change their password
            if (string.IsNullOrEmpty(model.OldPassword) && string.IsNullOrEmpty(model.NewPassword) && string.IsNullOrEmpty(model.ConfirmPassword))
            {
                // User is not trying to change their password and the password fields are removed from the ModelState
                // This is done so the model state is valid when the user is only changing their username and/or email
                ModelState.Remove("OldPassword");
                ModelState.Remove("NewPassword");
                ModelState.Remove("ConfirmPassword");
            }
            else // User is trying to change their password
            {
                // So we check each password field for null or empty values
                if (string.IsNullOrEmpty(model.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "You must enter your current password to change your password.");
                }
                if (string.IsNullOrEmpty(model.NewPassword))
                {
                    ModelState.AddModelError("NewPassword", "Please enter a new password.");
                }
                if (string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassword", "Please confirm your new password.");
                }
                // If all password fields are filled, we proceed...
                if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    if (await TryChangePassword(user, model))
                    {
                        return RedirectToAction("Settings");
                    }
                }
            }
            // If the user is not trying to change their password, we check if they changed their username and/or email
            // and attempt to update the user's information with our TryUpdateUser method.
            bool dataChanged = await TryUpdateUser(user, model);
            if (dataChanged)
            {
                return RedirectToAction("Settings");
            }
            // If the model state is not valid, or no data has changed, return the same view
            return View((model, user));
        }
        */
        // POST: Update the User's information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetails(UserSettingsViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // If the user is not trying to change their password, we check if they changed their username and/or email
            // and attempt to update the user's information with our TryUpdateUser method.
            bool dataChanged = await TryUpdateUser(user, model);
            if (dataChanged)
            {
                return RedirectToAction("Settings");
            }

            var scores = _context.Scores.Where(s => s.UserId == user.Id).ToList();

            // If the model state is not valid, or no data has changed, return the same view
            return View("Settings", (model, user, scores));
        }

        // POST: Update the User's password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserSettingsViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // User is trying to change their password
            // So we check each password field for null or empty values
            if (string.IsNullOrEmpty(model.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "You must enter your current password to change your password.");
            }
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("NewPassword", "Please enter a new password.");
            }
            if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Please confirm your new password.");
            }
            // If all password fields are filled, we proceed...
            if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                if (await TryChangePassword(user, model))
                {
                    return RedirectToAction("Settings");
                }
            }

            var scores = _context.Scores.Where(s => s.UserId == user.Id).ToList();

            // If the model state is not valid, return the same view
            return View("Settings", (model, user, scores));
        }

        // Helper methods that try to update the user's information and password
        private async Task<bool> TryChangePassword(User user, UserSettingsViewModel model)
        {
            // All password fields are filled, so try to change the password and call the _userManager.ChangePasswordAsync() method.
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded) // If the password change failed
            {
                // Add the errors to the ModelState
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError("OldPassword", error.Description);
                }
            }
            // Otherwise, return the result of the password change
            return changePasswordResult.Succeeded;
        }
        private async Task<bool> TryUpdateUser(User user, UserSettingsViewModel model)
        {
            bool dataChanged = false;

            // Check each property of the user to see if it has changed
            if (!string.IsNullOrEmpty(model.UserName) && !user.UserName.Equals(model.UserName))
            {
                user.UserName = model.UserName;
                dataChanged = true;
            }
            if (!string.IsNullOrEmpty(model.Email) && !user.Email.Equals(model.Email))
            {
                user.Email = model.Email;
                dataChanged = true;
            }

            // If any data has changed, update the user
            if (dataChanged)
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                    }
                }
                return result.Succeeded;
            }

            return false;
        }

        // POST: Users/SoftDelete
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] UserSettingsViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.IsDeleted)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Check if the password is correct
            var password = model.OldPassword;
            var passwordCheck = await VerifyPassword(currentUser, password);
            if (!passwordCheck)
            {
                return BadRequest("Incorrect password.");
            }

            currentUser.IsDeleted = true;
            var result = await _userManager.UpdateAsync(currentUser);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Game");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Settings", (new UserSettingsViewModel(), currentUser));
        }

        // Helper method that verifies the user's password
        private async Task<bool> VerifyPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

    }

}
