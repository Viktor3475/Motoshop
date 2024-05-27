using ApplicationData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationData.Context
{
    public class MotoShopDBContext:DbContext
    {
        public MotoShopDBContext(DbContextOptions<MotoShopDBContext> options):base(options)
        {

        }
        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
