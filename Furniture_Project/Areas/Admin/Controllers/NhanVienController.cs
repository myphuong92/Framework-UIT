﻿using Microsoft.AspNetCore.Mvc;

namespace Furniture_Project.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
