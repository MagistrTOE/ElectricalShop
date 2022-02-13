using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task AddProduct(Product product);
    }
}
