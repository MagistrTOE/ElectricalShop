using Data.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.Repositories
{
    public class CompanyRepository : RepositoryGeneric<Company>, ICompanyRepository
    {
        public CompanyRepository(MyElectricalShopContext contex) : base(contex)
        {
        }
    }
}
