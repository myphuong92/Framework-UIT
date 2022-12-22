using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Funiture_Project.Models;
using Funiture_Project.ModelViews;
using Funiture_Project.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MimeKit;
using MailKit.Net.Smtp;

namespace Funiture_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly FurnitureContext _context;
        public INotyfService _notyfService { get; }
        public ContactController(FurnitureContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("MaKH")==null)
            {
                return RedirectToAction("Index", "Login");
            }
            int makh = int.Parse(HttpContext.Session.GetString("MaKH"));
            var khachhang = _context.KhachHang.AsNoTracking()
                .Where(x => x.MaKh == makh)
                .FirstOrDefault();
            ViewBag.KhachHang = khachhang;
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(string Name, string Email, string Message)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Customer", "doantuquynh9.1px@gmail.com"));
            message.To.Add(new MailboxAddress("BiSys", "20521825@gm.uit.edu.vn"));
            message.Subject = "Ý kiến khách hàng";
            message.Body = new TextPart("plain")
            {
                Text = $"Họ tên: {Name} " +
                $"\nEmail: {Email}" +
                $"\nLời nhắn: " +
                $"\n{Message}"
            };
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("doantuquynh9.1px@gmail.com", "tzdabedwwjnvkhyv");
                client.Send(message);
                client.Disconnect(true);
            }
            //string subject = "Ý kiến khách hàng";
            //string body = $"Họ tên: {Name} " +
            //    $"\nEmail: {Email}" +
            //    $"\nLời nhắn: {Message}";
            //string to = "doantuquynh9.1px@gmail.com";
            //WebMail.Send(Email, subject, body, null, null, null, true, null, null, null, null, null, null);
            _notyfService.Success("Gửi góp ý thành công!");
            return RedirectToAction("Index", "Home");
        }
    }
}
