using MyElectricalShop.Identity.Domain.Models;

namespace MyElectricalShop.Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
    }
}
