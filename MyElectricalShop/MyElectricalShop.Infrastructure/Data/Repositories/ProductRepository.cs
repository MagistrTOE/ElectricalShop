using Data.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryGeneric<Product>, IProductRepository
    {
        public ProductRepository(MyElectricalShopContext context) : base(context)
        {
        }
    }
}
