using BuySite.Models.Entity;
using Project.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySite.Service.Interface
{
    public interface IProductRouteRepository
    {
        // 基礎資料庫操作
        void Add<T>(T entity) where T : class;
        void Edit<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        // City
        IEnumerable<Product> GetProduct(int id);

        Task<ShoppingCart> GrtShoppingCarByUserId(string userId);
    }
}
