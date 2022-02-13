using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task Add(Product product);
    }
}
