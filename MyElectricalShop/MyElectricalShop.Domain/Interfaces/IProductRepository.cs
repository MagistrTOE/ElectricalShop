using MyElectricalShop.Domain.Models;
using Data;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryGeneric<Product>
    {
        Task<List<Product>> GetProductsListWithFullInfo();
    }
}
