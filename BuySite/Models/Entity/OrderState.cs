using System.ComponentModel.DataAnnotations;

namespace BuySite.Models.Entity
{
    public class OrderState
    {
        [Key]
        public int StateId { get; set; }
        public string State { get; set; }
    }
}