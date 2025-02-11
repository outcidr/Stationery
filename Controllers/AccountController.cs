using Microsoft.AspNetCore.Mvc;
using Stationery.Models;
using Stationery.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
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
                    CreatedAt = DateTime.UtcNow,
                    Role = UserRole.User 
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);

            }
            return RedirectToAction("Index", "Home"); // Redirect to home after successful registration
        }


        // GET: /Account/AdminRegister
        public async Task<IActionResult> AdminRegister()
        {
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }

        // POST: /Account/AdminRegister
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(RegisterViewModel model)
        {
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
                var adminUser = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password),
                    CreatedAt = DateTime.UtcNow,
                    Role = UserRole.Admin 
                };
                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();

                _httpContext.HttpContext.Session.SetInt32("UserId", adminUser.Id);

            }
            return RedirectToAction("Index", "Home"); // Redirect to home after successful registration
        }


        //GET: /Account/Login
        public async Task<IActionResult> Login()
        {
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }

        //POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.Categories = await _context.Category.ToListAsync();

            if (ModelState.IsValid)
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail);

                if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }

                user.LastLogin = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);
                // Store Role in session if needed for authorization checks later
                _httpContext.HttpContext.Session.SetString("UserRole", user.Role.ToString());


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
            HttpContext.Session.Remove("UserRole"); // Remove role from session on logout
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