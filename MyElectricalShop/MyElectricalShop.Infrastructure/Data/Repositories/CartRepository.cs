using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;
using Data.EntityFrameworkCore;

namespace MyElectricalShop.Infrastructure.Data.Repositories
{
    public class CartRepository : RepositoryGeneric<Cart, Guid>, ICartRepository
    {
        public CartRepository(MyElectricalShopContext context) : base(context)
        {
        }

        public async Task<Cart> GetByUserId(Guid userId)
        {
            return await _entitySet
                .Where(x => x.UserId == userId)
                .Include(x => x.CartLines)
                .SingleOrDefaultAsync();
        }
    }
}
