using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Models;
using MyElectricalShop.Infrastructure.Data.ConfigurationsForModels;

namespace MyElectricalShop.Infrastructure
{
    public class MyElectricalShopContext : DbContext
    {
        public MyElectricalShopContext(DbContextOptions<MyElectricalShopContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VoltageLevelEntityConfiguration());
        }
    }
}
