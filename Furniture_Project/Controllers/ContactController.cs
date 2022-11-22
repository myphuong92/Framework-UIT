using Microsoft.AspNetCore.Mvc;

namespace Furniture_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
