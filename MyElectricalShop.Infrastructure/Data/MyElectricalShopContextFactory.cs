using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyElectricalShop.Infrastructure
{
    class MyElectricalShopContextFactory:IDesignTimeDbContextFactory<MyElectricalShopContext>
    {
        public MyElectricalShopContext CreateDbContext(string[] args)
        {
            var optionsBuilder= new DbContextOptionsBuilder<MyElectricalShopContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=MyElectricalShopDb;");
            return new MyElectricalShopContext(optionsBuilder.Options);
        }
    }
}
