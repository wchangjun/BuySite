﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Controllers
{
    public class ShoppingCartController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
