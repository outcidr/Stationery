using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Stationery.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/AdminHome/Index.cshtml");
        }
    }
}
