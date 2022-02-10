using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure
{
    public class MyElectricalShopContext : DbContext
    {
        public MyElectricalShopContext(DbContextOptions<MyElectricalShopContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
