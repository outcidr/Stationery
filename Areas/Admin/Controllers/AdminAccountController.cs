using Microsoft.AspNetCore.Mvc;
using Stationery.Models;
using Stationery.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Stationery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly StationeryContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public AdminAccountController(StationeryContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        //GET: /Admin/Account/Login
        public async Task<IActionResult> Login()
        {
            return View("~/Areas/Admin/Views/AdminAccount/Login.cshtml");
        }

        //POST: /Admin/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail);
                if (user != null && VerifyPassword(model.Password, user.PasswordHash))
                {
                    ModelState.AddModelError("", "Неправильне Ім'я користувача або пароль");
                    return View("~/Areas/Admin/Views/AdminAccount/Login.cshtml", model);
                }
                user.LastLogin = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);

                if (model.RememberMe)
                {
                    SetAuthCookie(user.Id);
                }
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            return View("~/Areas/Admin/Views/AdminAccount/Login.cshtml", model);
        }

        //POST: /Admin/Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            Response.Cookies.Delete("AuthCookie");
            return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        private void SetAuthCookie(int userID)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
        }
    }
}
