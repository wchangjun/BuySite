using BuySite.Models.Entity;
using BuySite.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly BuySiteDBContext _BuySiteDBContext;
        public OrderController(BuySiteDBContext context)
        {
            _BuySiteDBContext = context;

        }
        [HttpPost]         //如果要用json格式用加[FromBody]
        public object AddOrder([FromBody]OrderViewModel orderViewModel)
        {
            using (_BuySiteDBContext)
            {
                OrderDetile orderDetile=new OrderDetile
                {
                    Address = orderViewModel.Address,
                    Phone = orderViewModel.Phone,
                };
                _BuySiteDBContext.OrderDetiles.Add(orderDetile);
                _BuySiteDBContext.SaveChanges();
            }

                return Ok();
        }
        //public object AddUser()
        //{
        //    //var usrt= from c in _BuySiteDBContext.Orders 
        //    //return Ok()
        //}
        //public IActionResult orderdatil( )
        //{
            
        //}
    }
}
