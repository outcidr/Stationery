using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Stationery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
