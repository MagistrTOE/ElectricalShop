using Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryGeneric<Product>, IProductRepository
    {
        public ProductRepository(MyElectricalShopContext context) : base(context)
        {  
        }

        public async Task<List<Product>> GetProductsListWithFullInfo()
        {
            return await _entitySet
                .Include(x => x.Category)
                .Include(x => x.Company)
                .Include(x => x.VoltageLevel)
                .ToListAsync();
        }
    }
}
