using Microsoft.AspNetCore.Mvc;

namespace Furniture_Project.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
