using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
    }
}
