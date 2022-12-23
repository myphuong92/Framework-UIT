using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Funiture_Project.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Collections.Generic;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Funiture_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly FurnitureContext _context;
        public INotyfService _notifyService { get; }

        public AdminProductsController(FurnitureContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPham.ToListAsync());
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.DanhMucSp, "MaDm", "TenDm");

            SelectList NsxList = new SelectList(_context.SanPham.Select(s => s.Nsx).Distinct().ToList(), "MaSp", "Nsx");
            ViewData["NuocSx"] = NsxList;


            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,Nsx,ThuongHieu,Gia,TongSl,HinhAnh,MaDm,ChiTiet")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.DanhMucSp, "MaDm", "TenDm", sanPham.MaDm);

            SelectList NsxList = new SelectList(_context.SanPham.Select(s => s.Nsx).Distinct().ToList(), "MaSp", "Nsx", sanPham.Nsx);
            ViewData["NuocSx"] = NsxList;

            return View(sanPham);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.DanhMucSp, "MaDm", "TenDm", sanPham.MaDm);
            return View(sanPham);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,Nsx,ThuongHieu,Gia,TongSl,HinhAnh,MaDm,ChiTiet")] SanPham sanPham)
        {
            if (id != sanPham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSp))
                    {
                        _notifyService.Error("Cập nhật thất bại!");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.DanhMucSp, "MaDm", "TenDm", sanPham.MaDm);
            return View(sanPham);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.MaSp == id);
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.Name}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        SanPham sp = new SanPham
                        {
                            TenSp = reader.GetValue(0).ToString(),
                            Nsx = reader.GetValue(1).ToString(),
                            ThuongHieu = reader.GetValue(2).ToString(),
                            Gia = double.Parse(reader.GetValue(3).ToString()),
                            TongSl = int.Parse(reader.GetValue(4).ToString()),
                            HinhAnh = reader.GetValue(5).ToString(),
                            MaDm = reader.GetValue(6).ToString(),
                            ChiTiet = reader.GetValue(7).ToString()
                        };
                        _context.Add(sp);
                    }
                    try
                    {
                        _context.SaveChanges();
                        _notifyService.Success("Import thành công");
                    }
                    catch (Exception e)
                    {
                        _notifyService.Error(e.Message);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
