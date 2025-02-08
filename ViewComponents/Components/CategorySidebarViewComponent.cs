using Microsoft.AspNetCore.Mvc;
using Stationery.Data; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Stationery.ViewComponents
{
    public class CategorySidebarViewComponent : ViewComponent
    {
        private readonly StationeryContext _context;

        public CategorySidebarViewComponent(StationeryContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Category.ToListAsync();
            return View(categories); // Will return the "Default.cshtml" view
        }
    }
}
