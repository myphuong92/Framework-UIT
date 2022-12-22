using Microsoft.AspNetCore.Mvc;
using Funiture_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Funiture_Project.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly FurnitureContext _context;
        public ThongKeController(FurnitureContext context)
        {
            _context = context;
        }
        [Area("Admin")]
        public IActionResult Index()
        {
            List<double> doanhthu = new List<double>();
            for (int i = 1;i<=12;i++)
            {
                var dtthang = _context.HoaDon.AsNoTracking()
                    .Where(x => x.NgayHd.Month == i && x.Ttdh.Equals("Bị hủy") == false)
                    .Sum(x => x.TriGia);
                doanhthu.Add(dtthang);
            }
            ViewBag.Doanhthu = doanhthu;
            return View();
        }
    }
}
