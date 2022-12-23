using Microsoft.AspNetCore.Mvc;
using Funiture_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Funiture_Project.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        private readonly FurnitureContext _context;
        public INotyfService _notyfService { get; }
        public DonHangController(FurnitureContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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
        [Route("/DangCho_Update/{mahd}", Name = "DangCho_Update")]
        public IActionResult DangCho_Update(int mahd)
        {
            var hoadon = _context.HoaDon.AsNoTracking()
                .Where(x => x.MaHd == mahd)
                .FirstOrDefault();
            var khachhang = _context.KhachHang.AsNoTracking()
                .Where(x => x.MaKh == hoadon.MaKh)
                .FirstOrDefault();
            var cthd = _context.Cthd.AsNoTracking()
                .Where(x => x.MaHd == mahd)
                .ToList();
            List<SanPham> lsSanPham = new List<SanPham>();
            foreach (var item in cthd)
            {
                var sanpham = _context.SanPham.AsNoTracking()
                .Where(x => x.MaSp == item.MaSp)
                .FirstOrDefault();
                lsSanPham.Add(sanpham);
            }
            ViewBag.HoaDon = hoadon;
            ViewBag.KhachHang = khachhang;
            ViewBag.CTHD = cthd;
            ViewBag.lsSanPham = lsSanPham;
            return View();
        }

        [Area("Admin")]
        [Route("/HuyHoaDon/{mahd}", Name = "Cancel_Order")]
        public IActionResult HuyHoaDon(int mahd)
        {
            var hoadon = _context.HoaDon.AsNoTracking()
                .Where(x => x.MaHd == mahd)
                .FirstOrDefault();
            hoadon.Ttdh = "Bị hủy";
            _context.HoaDon.Update(hoadon);
            _context.SaveChanges();
            _notyfService.Success("Hủy hóa đơn thành công");
            return RedirectToAction("DangCho", "DonHang");
        }

        [Area("Admin")]
        [Route("/CapNhatTrangThai/{mahd}", Name = "CapNhatTrangThai")]
        public IActionResult CapNhatTrangThai(int mahd)
        {
            var hoadon = _context.HoaDon.AsNoTracking()
                .Where(x => x.MaHd == mahd)
                .FirstOrDefault();
            if (hoadon.Ttdh.Equals("Đang chờ"))
            {
                hoadon.Ttdh = "Đang giao";
            }
            else 
            {
                if (hoadon.Ttdh.Equals("Đang giao"))
                {
                    hoadon.Ttdh = "Đã giao";
                }
            } 
            _context.HoaDon.Update(hoadon);
            _context.SaveChanges();

            _notyfService.Success("Cập nhật trạng thái thành công");

            return RedirectToAction("DangCho","DonHang");
        }
    }
}
