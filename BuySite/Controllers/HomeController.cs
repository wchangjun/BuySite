using BuySite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BuySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "employee,admin")]
        //[Authorize]//沒登錄不會看到
        public IActionResult Privacy()
        {
            var data =HttpContext.User.Claims.Where(x => x.Type == "Hello").FirstOrDefault();
            if (data!= null)
            {
                ViewBag.Salary = data.Value;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult SendMail() 
        {
            //建立一個mail的物件
            var mail = new MailMessage();

            //把mail物件要的資訊寫完
            mail.From = new MailAddress("changjunwork@gmail.com");
            mail.To.Add("harry2355183@gmail.com");
            mail.Subject = "你看!穿山甲耶";
            mail.Body = "<h1>Hello</h1>";
            mail.IsBodyHtml = true;
            //請人送到指定的mail server
            SmtpClient client = new SmtpClient("smtp.gmail.com",587);
            client.Credentials = new System.Net.NetworkCredential("changjunwork@gmail.com", "zxcv6649");
            client.EnableSsl = true;

            client.Send(mail);
            return Content("寄信成功");
        }

    }
}
