using Microsoft.AspNetCore.Mvc;
using Stationery.Data;
using Stationery.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.Controllers
{
    public class SearchController : Controller
    {
        private readonly StationeryContext _context;

        public SearchController(StationeryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SearchResults(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;

            IQueryable<Product> query = _context.Product.Include(p => p.Category); // Use _context.Product (singular)

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm));
            }

            var searchResults = await query.ToListAsync();
            return View("~/Views/Product/SearchResults.cshtml", searchResults); // Explicitly specify the path to SearchResults view
        }
    }
}