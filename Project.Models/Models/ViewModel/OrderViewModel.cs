using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Models.ViewModel
{
    public class OrderViewModel
    {
        public string name { set; get; }
        public string ProductName { set; get; }
        public string Address { set; get; }
        public string Phone { get; set; }
    }
}
