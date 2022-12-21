using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Models.ViewModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuySite.Service
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        public OrdersController(IHttpContextAccessor httpContextAccessor, ProductRepository productRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //使用用戶id來獲取訂單歷史紀錄
            var order = _productRepository.GetOrdersByUserId(userid);
            return Ok(_mapper.Map<IEnumerable<OrdersDto>>(userid));
        }
        //[HttpGet("{orderId}")]
        //public async Task<IActionResult> GetOrderById([FromRoute]Guid orderId)
        //{
        //    var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //}
    }
}
