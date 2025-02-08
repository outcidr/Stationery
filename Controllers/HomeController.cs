using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stationery.Data;
using Stationery.Models;
using Microsoft.EntityFrameworkCore;


namespace Stationery.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly StationeryContext _context;

        public HomeController(StationeryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                
                HeroImageUrl = "images/hero.jpg"
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
