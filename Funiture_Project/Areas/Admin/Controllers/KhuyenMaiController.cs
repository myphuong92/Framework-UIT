using Microsoft.AspNetCore.Mvc;

namespace Funiture_Project.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
