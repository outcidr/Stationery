using Microsoft.AspNetCore.Mvc;
using Stationery.Models;
using Stationery.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Stationery.Areas.Admin.Models;

namespace Stationery.Controllers
{
    public class AccountController : Controller
    {
        private readonly StationeryContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public AccountController(StationeryContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }



        // GET: /Account/Register
        public async Task<IActionResult> Register()
        {
            // Fetch categories for layout
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Fetch categories for layout (even on postback)
            ViewBag.Categories = await _context.Category.ToListAsync();

            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View(model);
                }
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email already registered");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password),
                    CreatedAt = DateTime.UtcNow
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        //GET: /Account/Login
        public async Task<IActionResult> Login()
        {
            // Fetch categories for layout
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }
        //POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Fetch categories for layout (even on postback)
            ViewBag.Categories = await _context.Category.ToListAsync();

            if (ModelState.IsValid)
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail ||
                u.Email == model.UsernameOrEmail);
                if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }
                user.LastLogin = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);
                if (model.RememberMe)
                {
                    SetAuthCookie(user.Id);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        //POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            Response.Cookies.Delete("AuthCookie");
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        private void SetAuthCookie(int userId)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true
            };
        }


    }
}