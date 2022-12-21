using BuySite.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.Entity
{
    public class ShoppingCart
    {   
        [Key]
        public Guid Id { get; set; }
        public string Userid { get; set; }
        public User User { get; set; }
        public ICollection<LineItem> ShoopingCarItems { get; set; }
    }
}
