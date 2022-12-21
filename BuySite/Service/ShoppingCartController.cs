using AutoMapper;
using BuySite.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Project.Models.Models.Entity;
using Project.Models.Models.ViewModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuySite.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BuySiteDBContext _buySiteDBContext;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ShoppingCartController(IHttpContextAccessor httpContextAccessor, BuySiteDBContext buySiteDBContext, ProductRepository productRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _buySiteDBContext = buySiteDBContext;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetShoppingCart(string ProductId,string UserId, int Product)
        //{
        //    //1.獲得當前用戶
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //2.使用userid獲得購物車

        //    var productDate = (from product in _buySiteDBContext.Products
        //                       where product.Id == Product
        //                       select new { product }
        //                       ).FirstOrDefault();

        //    var userData = _buySiteDBContext.Users.FirstOrDefault(x => x.Id == UserId);
        //}
        //[HttpGet("{id}:int")]
        [HttpGet("customers/{id}/orders")]
        public IActionResult AddtoCart(int id)
        {
            //取得商品資料
            CartItem Item = new CartItem
            {
                Product = _buySiteDBContext.Products.Single(x => x.Id.Equals(id)),
                Amount = 1,
                SubTotal = _buySiteDBContext.Products.Single(m => m.Id == id).Price
            };
            //判斷 Session 內有無購物車
            if (HttpContext.Session.GetString("cart") == null)
            {
                //如果沒有已存在購物車: 建立新的購物車
                List<CartItem> cart = new List<CartItem>();
                cart.Add(Item);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(Item));
            }
            else
            {
                var shoppjson = HttpContext.Session.GetString("cart");
                List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(shoppjson);

                int index = cart.FindIndex(m => m.Product.Id.Equals(id));
                if (index != 1)
                {
                    cart[index].Amount += Item.Amount;
                    cart[index].SubTotal += Item.SubTotal;
                }
                else
                {
                    cart.Add(Item);
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            }
            return NoContent();
        }
        public async Task<IActionResult> GetShoppingCart(string userid)
        {
            //獲得當前用戶
            //var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //使用者userid獲得購物車
            var shoppingCart = await _productRepository.GrtShoppingCarByUserId(userid);

            return Ok(_mapper.Map<ShoppingCartViewModel>(shoppingCart));
        }
        [HttpPost("items")]
        public async Task<IActionResult> AddShoppingCartItem([FromBody] AddShoppingCarItemDto addShoppingCarItemDto, string userid)
        {
            //獲得當前用戶    
            //var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //使用者userid獲得購物車
            var shoppingCart = await _productRepository.GrtShoppingCarByUserId(userid);
            //創建lineItem
            var product = await _productRepository.GetProductAsync(addShoppingCarItemDto.ProductId);
            if (product == null)
            {
                return NotFound("產品不存在");
            }
            var lineItem = new LineItem()
            {
                ProductId = addShoppingCarItemDto.ProductId,
                ShoppingCartId = shoppingCart.Id,
                Price = product.Price,
            };
            await _productRepository.AddShoppingCartItem(lineItem);
            await _productRepository.SaveAsync();
            return Ok(_mapper.Map<ShoppingCartViewModel>(shoppingCart));
        }
        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteShoppingCarItem([FromRoute] int itemId)
        {
            //使用者userid獲得購物車
            //var shoppingCart = await _productRepository.GrtShoppingCarByUserId(userid);
            //獲取lineitem數據
            var lineItem = await _productRepository.GetShoppingCarItemId(itemId);
            if (lineItem == null)
            {
                return NotFound("購物車商品找不到");
            }
            _productRepository.DeleteShoppingCartItem(lineItem);
            await _productRepository.SaveAsync();
            return NoContent();

        }
        [HttpDelete("items/({itemIDs})")]
        public async Task<IActionResult> RemoveShoppingCartItems(/*[ModelBinder(BinderType = typeof(ArrayModelBinder<int>))]*/[FromRoute] IEnumerable<int> itemIDs)
        {
            var lineitem = await _productRepository.GetshoppingCartsByIdListAsync(itemIDs);
            _productRepository.DeleteShoppingCarItems(lineitem);
            await _productRepository.SaveAsync();
            return NoContent();
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _productRepository.GrtShoppingCarByUserId(userid);
            //創建訂單
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                Userid = userid,
                State = OrderStateEnum.Pending,
                CreateDateUTC = DateTime.UtcNow
            };

            //保存數據
            await _productRepository.AddOrderAsync(order);
            await _productRepository.SaveAsync();
            return Ok(_mapper.Map<OrdersDto>(order));
        }

    }
}
