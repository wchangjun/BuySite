using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.ViewModel
{
   public class ProductViewModel
    {
        public int Id { get; set; }
        //商品建立時間
        public DateTime CreateTime { get; set; } = DateTime.UtcNow.AddHours(8);
        //商品結束時間
        public DateTime EndTime { get; set; } = DateTime.UtcNow.AddHours(8).AddDays(10);
        //商品名稱

        public string Name { get; set; }
        //商品描述
        public string Description { get; set; }
        //商品內容
        public string Content { get; set; }
        //商品價格
        public decimal Price { get; set; }
        //商品達標金額
        public decimal Total { get; set; }
        //商品類別
        public string Category { get; set; }
        //商品軟刪除
        public bool Disable { get; set; } = false;
    }
}
