using Furniture_Project.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Furniture_Project.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
