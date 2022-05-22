using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Models.Entity
{
    public class BuySiteDBContext:DbContext
    {
        public BuySiteDBContext(DbContextOptions<BuySiteDBContext> options): base(options) { }
        public virtual DbSet<OrderDetile> Orders { set; get; }
        public virtual DbSet<OrderState> OrderStates { set; get; }
        public virtual DbSet<User> Users { set; get; }
    }
}
