using BuySite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.Entity
{
    public class OrderItem
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal SubTotal { get; set; }
    }
    public class CartItem : OrderItem
    { 
    public Product Product { get; set; }
    }
}
