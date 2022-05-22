using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Controllers
{
    public class LoginAndRegisterController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //註冊畫面
        public IActionResult Register()
        {
            return View();
        }
        //登入畫面
        public IActionResult Login()
        {
            return View();
        }
    }
}
