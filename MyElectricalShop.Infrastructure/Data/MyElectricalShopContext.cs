using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure
{
    public class MyElectricalShopContext:DbContext
    {
        public MyElectricalShopContext(DbContextOptions<MyElectricalShopContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
