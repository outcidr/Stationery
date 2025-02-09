using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stationery.Areas.Admin.Models; 
using System.Threading.Tasks;

namespace Stationery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email }; // Create IdentityUser
                var result = await _userManager.CreateAsync(user, model.Password); // Use UserManager to create user

                if (result.Succeeded)
                {
                    // Optionally, you can assign a role to the admin user here, e.g., await _userManager.AddToRoleAsync(user, "Admin");
                    await _signInManager.SignInAsync(user, isPersistent: false); // Sign in after registration
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" }); // Redirect to Admin Home
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); // Add Identity errors to ModelState
                }
            }
            return View(model); // Return to Register view with errors
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UsernameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: false); // Use SignInManager for password login
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" }); // Redirect to Admin Home
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Login failed
            }
            return View(model);
            // Return to Login view with error
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Require authentication for Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Use SignInManager to sign out
            return RedirectToAction("Index", "Home", new { area = "" }); // Redirect to Home page
        }
    }
}