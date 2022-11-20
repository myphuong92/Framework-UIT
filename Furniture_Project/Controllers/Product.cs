using Microsoft.AspNetCore.Mvc;

namespace Furniture_Project.Controllers
{
    public class Product : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
