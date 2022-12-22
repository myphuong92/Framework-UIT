using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Funiture_Project.Models;
using Funiture_Project.ModelViews;
using Funiture_Project.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace Funiture_Project.Controllers
{
    public class PaymentController : Controller
    {
        private readonly FurnitureContext _context;
        public INotyfService _notyfService { get; }
        public PaymentController(FurnitureContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        
        public IActionResult Index()
        {
            int makh = int.Parse(HttpContext.Session.GetString("MaKH"));
            KhachHang khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == makh)
                    .FirstOrDefault();
            List<SanPham> lsSanPham = new List<SanPham>();
            var giohang = _context.GioHang.AsNoTracking()
                .Where(x => x.MaKh == makh)
                .ToList();

            double tong = 0;

            foreach (var item in giohang)
            {
                var sanpham = _context.SanPham.AsNoTracking()
                    .Where(x => x.MaSp == item.MaSp)
                    .FirstOrDefault();
                if (item.SoLuong <= sanpham.TongSl)
                {
                    sanpham.TongSl = item.SoLuong;
                    lsSanPham.Add(sanpham);
                    tong += sanpham.Gia * sanpham.TongSl;
                }
                else
                {
                    sanpham.TongSl = 0;
                }
            }
            if (lsSanPham.Count == 0)
            {
                _notyfService.Error("Đơn hàng rỗng");
                return RedirectToAction("Index", "CartInfo");
            }
            var khuyenmai = _context.KhuyenMai.AsNoTracking()
                .Where(x => x.NgayBd <= DateTime.Now && x.NgayKt >= DateTime.Now && x.DinhMuc <= tong);
            double max_tiengiam = 0;
            double tiengiam = 0;
            int makm = -1;
            foreach (var item in khuyenmai)
            {
                if (tong * item.PhanTramKm > item.DinhMuc)
                    tiengiam = item.DinhMuc;
                else
                    tiengiam = tong * item.PhanTramKm;
                if (tiengiam > max_tiengiam)
                {
                    max_tiengiam = tiengiam;
                    makm = item.MaKm;
                }
            }
            var km = _context.KhuyenMai.AsNoTracking()
                .Where(x => x.MaKm == makm)
                .FirstOrDefault();
            ViewBag.lsSanPham = lsSanPham;
            ViewBag.KhachHang = khachhang;
            ViewBag.KhuyenMai = km;
            ViewBag.Tong = tong;
            ViewBag.TongThanhTien = tong - max_tiengiam;
            return View();
        }

        [HttpPost]
        [Route("/{masp}")]
        public IActionResult Index(int masp, int amount = 1)
        {
            if(HttpContext.Session.GetString("MaKH")==null)
            {
                return RedirectToAction("Index", "Login");
            }
            int makh = int.Parse(HttpContext.Session.GetString("MaKH"));
            KhachHang khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == makh)
                    .FirstOrDefault();
            
            double tong = 0;
            var sanpham = _context.SanPham.AsNoTracking()
                .Where(x => x.MaSp == masp)
                .FirstOrDefault();
            sanpham.TongSl = amount;
            tong = sanpham.Gia * amount;

            var khuyenmai = _context.KhuyenMai.AsNoTracking()
                .Where(x => x.NgayBd <= DateTime.Now && x.NgayKt >= DateTime.Now && x.DinhMuc <= tong);
            double max_tiengiam = 0;
            double tiengiam = 0;
            int makm = -1;
            foreach (var item in khuyenmai)
            {
                if (tong * item.PhanTramKm > item.DinhMuc)
                    tiengiam = item.DinhMuc;
                else
                    tiengiam = tong * item.PhanTramKm;
                if (tiengiam > max_tiengiam)
                {
                    max_tiengiam = tiengiam;
                    makm = item.MaKm;
                }
            }
            var km = _context.KhuyenMai.AsNoTracking()
                .Where(x => x.MaKm == makm)
                .FirstOrDefault();

            GioHang gh = new GioHang();
            gh.MaKh = makh;
            gh.MaSp = masp;
            gh.SoLuong = amount;

            List<SanPham> lssanpham = new List<SanPham>();
            lssanpham.Add(sanpham);

            HttpContext.Session.Set<GioHang>("CartItem", gh);

            ViewBag.lsSanPham = lssanpham;
            ViewBag.KhachHang = khachhang;
            ViewBag.KhuyenMai = km;
            ViewBag.Tong = tong;
            ViewBag.TongThanhTien = tong - max_tiengiam;
            return View();
        }

        [Route("/Payment/InsertHoaDon")]
        public IActionResult InsertHoaDon()
        {
            int makh = int.Parse(HttpContext.Session.GetString("MaKH"));
            var khachhang = _context.KhachHang.AsNoTracking()
                    .Where(x => x.MaKh == makh)
                    .FirstOrDefault();
            var new_hoadon = new HoaDon
            {
                MaKh = makh,
                Ttdh = "Đang chờ",
                Tttt = "Chưa thanh toán",
                DiaChi = khachhang.DiaChi,
                Sdt = khachhang.Sdt,
                NgayHd = DateTime.Now,
                NgayGh = DateTime.Now,
                ThanhPho = khachhang.ThanhPho
            };
            _context.HoaDon.Add(new_hoadon);
            _context.SaveChanges();
            var mahd = _context.HoaDon.AsNoTracking()
                .OrderByDescending(x => x.MaHd)
                .Select(x => x.MaHd)
                .FirstOrDefault();
            double thanhtien = 0;

            if (HttpContext.Session.Get<GioHang>("CartItem") == null) 
            {
                var giohang = _context.GioHang.AsNoTracking()
                .Where(x => x.MaKh == makh).ToList();
                foreach (var item in giohang)
                {
                    var sanpham = _context.SanPham.AsNoTracking()
                        .Where(x => x.MaSp == item.MaSp)
                        .FirstOrDefault();
                    if (sanpham.TongSl > item.SoLuong)
                    {
                        var new_cthd = new Cthd
                        {
                            MaHd = mahd,
                            MaSp = item.MaSp,
                            SoLuong = item.SoLuong,
                            DonGia = sanpham.Gia
                        };
                        thanhtien += new_cthd.DonGia * new_cthd.SoLuong;

                        _context.Cthd.Add(new_cthd);
                        var gh = _context.GioHang.AsNoTracking()
                            .Where(x => x.MaSp == item.MaSp && x.MaKh == makh);

                        sanpham.TongSl -= item.SoLuong;
                        _context.SanPham.Update(sanpham);

                        _context.GioHang.Remove(item);
                    }

                }
            }
            else
            {
                var giohang = HttpContext.Session.Get<GioHang>("CartItem");
                var sanpham = _context.SanPham.AsNoTracking()
                        .Where(x => x.MaSp == giohang.MaSp)
                        .FirstOrDefault();
                if (sanpham.TongSl > giohang.SoLuong)
                {
                    var new_cthd = new Cthd
                    {
                        MaHd = mahd,
                        MaSp = giohang.MaSp,
                        SoLuong = giohang.SoLuong,
                        DonGia = sanpham.Gia
                    };
                    thanhtien += new_cthd.DonGia * new_cthd.SoLuong;

                    _context.Cthd.Add(new_cthd);
                    
                    sanpham.TongSl -= giohang.SoLuong;
                    _context.SanPham.Update(sanpham);
                }
            }
            
            var khuyenmai = _context.KhuyenMai.AsNoTracking()
                .Where(x => x.NgayBd <= new_hoadon.NgayHd && x.NgayKt >= new_hoadon.NgayHd && x.DinhMuc <= thanhtien);
            double max_tiengiam = 0;
            double tiengiam = 0;
            foreach (var item in khuyenmai)
            {
                if (thanhtien * item.PhanTramKm > item.DinhMuc)
                    tiengiam = item.DinhMuc;
                else
                    tiengiam = thanhtien * item.PhanTramKm;
                if (tiengiam > max_tiengiam) 
                    max_tiengiam = tiengiam;
            }
            thanhtien -= max_tiengiam;
            new_hoadon.TriGia = thanhtien;
            _context.HoaDon.Update(new_hoadon);

            _context.SaveChanges();

            _notyfService.Success("Đặt hàng thành công");
            return RedirectToAction("Index", "Home");
        }

    }
}
