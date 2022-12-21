using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuySite.Models.Entity
{
    [Table("OrderDetile")]
    public class OrderDetile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //訂單ID
        public string Id { get; set; }
        //訂單建立時間 
        public DateTime? CreateDate { get; set; }
        //訂單軟刪除
        public bool? Disable { get; set; } = false;
        //訂單地址
        public string Address { get; set; }
        //訂單連絡電話
        public string Phone { get; set; }

        [ForeignKey("OrderState")]
        public int? StateId { get; set; }
        public virtual OrderState OrderState { get; set; }
    }
}