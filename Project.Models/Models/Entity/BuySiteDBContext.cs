using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models.Models.Entity;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuySite.Models.Entity
{
    public class BuySiteDBContext: IdentityDbContext<User>
    {
        public BuySiteDBContext(DbContextOptions<BuySiteDBContext> options): base(options) { }
        public virtual DbSet<OrderDetile> OrderDetiles { set; get; }
        public virtual DbSet<OrderState> OrderStates { set; get; }
        public override DbSet<User>  Users { set; get; }
        public DbSet<Order> Orders { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public DbSet<ShoppingCart> ShoppingCarts { set; get; }
        public DbSet<LineItem> LineItems { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            //modelBuilder.Entity<TouristRoutePrice>().ToTable("TouristRoutePrice");
            //modelBuilder.Entity<TouristRoute>().ToTable("TouristRoute");

            //modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "ceshititle",
            //    Description = "shuoming",
            //    OriginalPrice = 0,
            //    CreateTime = DateTime.UtcNow
            //});
            //讀取json檔案資料存取到資料庫
            //var touristRouteJsonData =  File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/touristRoutesMockData.json");
            //IList<TouristRoute> touristRoute = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData);
            //modelBuilder.Entity<TouristRoute>().HasData(touristRoute);
            base.OnModelCreating(modelBuilder);
        }
    }
}
