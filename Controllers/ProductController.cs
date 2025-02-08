using Microsoft.AspNetCore.Mvc;
using Stationery.Data;
using Stationery.Models; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Stationery.Controllers
{
    public class ProductController : Controller
    {
        private readonly StationeryContext _context;

        public ProductController(StationeryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var products = await _context.Product.Include(p => p.Category).ToListAsync(); 
            return View(products); 
        }
    }
}