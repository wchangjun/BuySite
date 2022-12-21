using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.ViewModel
{
   public class LineItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; } 
        public ProductViewModel Product { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public decimal Price { get; set; }
    }
}
