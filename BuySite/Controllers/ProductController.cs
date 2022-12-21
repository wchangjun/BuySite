using BuySite.Models.Entity;
using BuySite.Models.ViewModel;
//using BuySite.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Common.Helper;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment1;
        private readonly BuySiteDBContext _buySiteDBContext;
        private readonly ProductRepository _productRepository;
        private readonly AppInfo _appinfo;
        private readonly IConfiguration _configruration;
        private readonly IDbConnection _conn;

        public ProductController(IWebHostEnvironment environment ,BuySiteDBContext buySiteDBContext, ProductRepository productRepository, IConfiguration configruration, AppInfo appinfo, IDbConnection conn)
        {
            _environment1 = environment;
            _buySiteDBContext = buySiteDBContext;
            _configruration = configruration;
            _productRepository = productRepository;
            _appinfo = appinfo;
            _conn = conn;
            _productRepository = new ProductRepository(_appinfo, _configruration, _conn, _buySiteDBContext);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile([FromBody] UploadFileViewModel model)
        {
            var path = _environment1.WebRootPath;
            var pic = model.Pic.FirstOrDefault();
            var fileName = DateTime.Now.Ticks.ToString() + pic.FileName; //Tick怕有人新增同樣的檔名
            using (var fs = System.IO.File.Create($"{path}/{ fileName}"))
            {
                pic.CopyTo(fs);
            }
            //_db.Products.Add(new Product()
            //{
            //    Title = model.Title,
            //    Price = model.Price,
            //    Description = model.Description,
            //    PicPath = fileName
            //});

            return View();
        }
        //public OrderViewModel order(OrderViewModel model)
        //{
        //    model.Address = _db.OrderStates.Where(_ => _.State == model.Address).FirstOrDefault();
        //}
        //public IActionResult ExcelUpload()
        //{
        //    string name = Guid.NewGuid().ToString();
        //    string SubName=name.Substring()

        //    return View();
        //}
        public  IEnumerable<Product> GetProduct()
        {
            var result = _productRepository.GetProduct();
            return result;
        }
    }
}
