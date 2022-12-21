using BuySite.Models.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Common.Helper;
using Project.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository
    {

        private readonly IConfiguration _configruration;
        private readonly BuySiteDBContext _buySiteDBContext;
        private readonly AppInfo _appinfo;
        private readonly IDbConnection _conn;

        public ProductRepository(AppInfo appinfo, IConfiguration configruration, IDbConnection conn, BuySiteDBContext buySiteDBContext)
        {
            _appinfo = appinfo;
            _configruration = configruration;
            _buySiteDBContext = buySiteDBContext;
            _appinfo = new AppInfo(_configruration);
            _conn = conn;
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _buySiteDBContext.Products.Where(x =>x.Id == productId).FirstOrDefaultAsync();
        }
        public IEnumerable<Product> GetProduct()
        {
            var result = _conn.Query<Product>("SELECT * FROM Products");
            return result;
        }
        public async Task CreatShoppingCart(ShoppingCart shoppingCart)
        {
            await _buySiteDBContext.ShoppingCarts.AddAsync(shoppingCart);
            //await _buySiteDBContext.SaveChangesAsync();
        }
        public async Task<ShoppingCart> GrtShoppingCarByUserId(string userid)
        {
            //string strsql = "SELECT * FROM ShoppingCarts s join AspNetUsers u on s.Userid = u.Id join Products p on s.Userid = p.UserId Where s.Userid = @userid";
            //var results = _conn.Query<ShoppingCart>(strsql, new { userid = new[] { userid } }).ToList();
            return await _buySiteDBContext.ShoppingCarts
                .Include(x => x.User)
                .Include(x => x.ShoopingCarItems)
                .ThenInclude(li => li.Product)
                .Where(s => s.Userid == userid)
                .FirstOrDefaultAsync();
               
            //var redult =  _conn.Query<ShoppingCart>("SELECT * FROM ShoppingCarts s join AspNetUsers u on s.Userid = u.Id join Products p on s.Userid = p.UserId Where s.Userid = @userid");

            //_buySiteDBContext.ShoppingCarts.Where(x=>x.Userid == userid).Join(_buySiteDBContext.Users, x => x.Userid , c => c.Id,(x,c)=>new { 
            
            
            //})
            //return results;

        }
        public async Task<LineItem> GetShoppingCarItemId(int lineItemId)
        {
            return await _buySiteDBContext.LineItems.Where(s => s.Id == lineItemId).FirstOrDefaultAsync();
        }
        public async void DeleteShoppingCartItem(LineItem lineItem)
        {
             _buySiteDBContext.LineItems.Remove(lineItem);
        }
        public async Task AddShoppingCartItem(LineItem lineItem)
        {
            await _buySiteDBContext.LineItems.AddAsync(lineItem);
        }
        public async Task AddOrderAsync(Order order)
        {
            await _buySiteDBContext.Orders.AddAsync(order);
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userid)
        {
            return await _buySiteDBContext.Orders.Where(x => x.Userid == userid).ToListAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return (await _buySiteDBContext.SaveChangesAsync() >= 0);
        }
        public async Task<IEnumerable<LineItem>> GetshoppingCartsByIdListAsync(IEnumerable<int> ids)
        {
            return await _buySiteDBContext.LineItems.Where(li => ids.Contains(li.Id)).ToListAsync();
        }
        public void DeleteShoppingCarItems(IEnumerable<LineItem> lineItems)
        {
            _buySiteDBContext.LineItems.RemoveRange(lineItems);
        }
    }
}
