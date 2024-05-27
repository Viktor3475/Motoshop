using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationData.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<MotoShopDBContext>
    {
        public MotoShopDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MotoShopDBContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PC\\Documents\\MotoshopDB.mdf;Integrated Security=True;Connect Timeout=30");

            return new MotoShopDBContext(optionsBuilder.Options);
        }
    }
}
