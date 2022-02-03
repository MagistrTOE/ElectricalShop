using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MyElectricalShop.Domain;

namespace MyElectricalShop.Infrastructure.Repositories
{
    public class ProductRepository:Domain.Interfaces.IProductRepository
    {
        protected MyElectricalShopContext context;
        public ProductRepository(MyElectricalShopContext ctx)
        {
            context = ctx;
        }

        public Task<List<Domain.Models.Product>> GetAllProducts()
        {
            return context.Products.ToListAsync();
        }
    }
}
