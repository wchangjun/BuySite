using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.ViewModel
{
   public class OrdersDto
    {
        public Guid Id { get; set; }
        public string Userid { get; set; }
        public ICollection<LineItemViewModel> OrderItems { get; set; }
        public string State { get; set; }
        public DateTime CreateDateUTC { get; set; }
        public string TransactionMatadata { get; set; }
    }
}
