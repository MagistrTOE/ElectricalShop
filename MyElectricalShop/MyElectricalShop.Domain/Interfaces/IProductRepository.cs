using Data;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryGeneric<Product, Guid>
    {
        Task<List<Product>> GetProductsListWithFullInfo();
    }
}
