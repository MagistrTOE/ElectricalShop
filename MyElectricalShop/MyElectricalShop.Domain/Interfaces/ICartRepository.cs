using Data;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface ICartRepository : IRepositoryGeneric<Cart, Guid>
    {
        Task<Cart> GetByUserId(Guid userId);
    }
}
