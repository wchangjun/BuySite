using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Models.ViewModel
{
    public class UploadFileViewModel
    {
        public string Title { set; get; }
        public decimal Price { set; get; }
        public string Description { set; get; }
        //用大籃List接  不知道前端要傳幾張照片來
        public List<IFormFile> Pic { set; get; }
    }
}
