using BuySite.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public Guid Id { get; set; }
        public string Userid { get; set; }
        public User User { get; set; }
        public ICollection<LineItemViewModel> ShoopingCarItems { get; set; }
    }
}
