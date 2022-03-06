using MyElectricalShop.Domain.Models;
using Data;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface ICartRepository : IRepositoryGeneric<Cart, Guid>
    {
        Task<Cart> GetByUserId(Guid userId);
    }
}
