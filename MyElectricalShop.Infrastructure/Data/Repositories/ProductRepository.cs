using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected MyElectricalShopContext _context;
        protected DbSet<Product> _entitySet;
        public ProductRepository(MyElectricalShopContext context)
        {
            _context = context;
            _entitySet = context.Set<Product>();
        }
        public Task<List<Product>> GetAllProducts()
        {
            return _context.Products.ToListAsync();
        }
        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
