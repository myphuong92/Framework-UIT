using Microsoft.AspNetCore.Mvc;
using Funiture_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Funiture_Project.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        private readonly FurnitureContext _context;
        public DonHangController(FurnitureContext context)
        {
            _context = context;
        }
        [Area("Admin")]
        public IActionResult DangCho()
        {
            var model = _context.HoaDon.AsNoTracking()
                .Where(x => x.Ttdh.Equals("Đang chờ"));
            List<string> lsKhachHang = new List<string>();
            foreach (var item in model)
            {
                var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == item.MaKh)
                    .Select(x=>x.HoTen)
                    .FirstOrDefault();
                if(khachhang!=null)
                {
                    lsKhachHang.Add(khachhang);
                }
                else
                {
                    lsKhachHang.Add(item.MaKh.ToString());
                }
            }
            ViewBag.lsKhachHang = lsKhachHang;
            return View(model);
        }

        [Area("Admin")]
        public IActionResult DangGiao()
        {
            var model = _context.HoaDon.AsNoTracking()
                .Where(x => x.Ttdh.Equals("Đang giao"));
            List<string> lsKhachHang = new List<string>();
            foreach (var item in model)
            {
                var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == item.MaKh)
                    .Select(x => x.HoTen)
                    .FirstOrDefault();
                if (khachhang != null)
                {
                    lsKhachHang.Add(khachhang);
                }
                else
                {
                    lsKhachHang.Add(item.MaKh.ToString());
                }
            }
            ViewBag.lsKhachHang = lsKhachHang;
            return View(model);
        }

        [Area("Admin")]
        public IActionResult DaGiao()
        {
            var model = _context.HoaDon.AsNoTracking()
                .Where(x => x.Ttdh.Equals("Đã giao"));
            List<string> lsKhachHang = new List<string>();
            foreach (var item in model)
            {
                var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == item.MaKh)
                    .Select(x => x.HoTen)
                    .FirstOrDefault();
                if (khachhang != null)
                {
                    lsKhachHang.Add(khachhang);
                }
                else
                {
                    lsKhachHang.Add(item.MaKh.ToString());
                }
            }
            ViewBag.lsKhachHang = lsKhachHang;
            return View(model);
        }

        [Area("Admin")]
        public IActionResult BiHuy()
        {
            var model = _context.HoaDon.AsNoTracking()
                .Where(x => x.Ttdh.Equals("Bị hủy"));
            List<string> lsKhachHang = new List<string>();
            foreach (var item in model)
            {
                var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == item.MaKh)
                    .Select(x => x.HoTen)
                    .FirstOrDefault();
                if (khachhang != null)
                {
                    lsKhachHang.Add(khachhang);
                }
                else
                {
                    lsKhachHang.Add(item.MaKh.ToString());
                }
            }
            ViewBag.lsKhachHang = lsKhachHang;
            return View(model);
        }

        [Area("Admin")]
        public IActionResult TatCa()
        {
            var model = _context.HoaDon.AsNoTracking();
            List<string> lsKhachHang = new List<string>();
            foreach (var item in model)
            {
                var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == item.MaKh)
                    .Select(x => x.HoTen)
                    .FirstOrDefault();
                if (khachhang != null)
                {
                    lsKhachHang.Add(khachhang);
                }
                else
                {
                    lsKhachHang.Add(item.MaKh.ToString());
                }
            }
            ViewBag.lsKhachHang = lsKhachHang;
            return View(model);
        }

        [Area("Admin")]
        public IActionResult DangCho_Update(string mahd)
        {

            return View();
        }
    }
}
