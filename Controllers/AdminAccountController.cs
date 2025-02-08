using Microsoft.AspNetCore.Mvc;
using Stationery.Data;
using Microsoft.EntityFrameworkCore;
using Stationery.Models.Admin;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Stationery.Controllers // Correct namespace: Stationery.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly StationeryContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public AdminAccountController(StationeryContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        // GET: /AdminAccount/Register
        public async Task<IActionResult> Register()
        {
            ViewBag.Categories = await _context.Category.ToListAsync(); // Fetch categories for layout
            return View();
        }

        // POST: /AdminAccount/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            ViewBag.Categories = await _context.Category.ToListAsync();
            if (ModelState.IsValid)
            {
                if (await _context.AdminAccounts.AnyAsync(a => a.Username == model.Username))
                {
                    ModelState.AddModelError("", "Admin Username already exists");
                    return View(model);
                }
                if (await _context.AdminAccounts.AnyAsync(a => a.Email == model.Email))
                {
                    ModelState.AddModelError("", "Admin Email already registered");
                    return View(model);
                }
                var adminAccount = new AdminAccount
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password),
                    Role = model.Role ?? "Admin"
                };
                _context.AdminAccounts.Add(adminAccount);
                await _context.SaveChangesAsync();
                _httpContext.HttpContext.Session.SetInt32("AdminId", adminAccount.Id);
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" }); // Redirect to Admin Home in Admin Area
            }
            return View(model);
        }

        //GET: /AdminAccount/Login
        public async Task<IActionResult> Login()
        {
            ViewBag.Categories = await _context.Category.ToListAsync();
            return View();
        }

        //POST: /AdminAccount/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminId");
            Response.Cookies.Delete("AdminAuthCookie");
            return RedirectToAction("Index", "Home", new { area = "" }); // Redirect to Home page (non-admin area)
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}