using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
