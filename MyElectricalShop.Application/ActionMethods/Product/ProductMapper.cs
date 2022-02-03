using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;
using Products = MyElectricalShop.Domain.Models.Product;

namespace MyElectricalShop.Application.ActionMethods.Product
{
    public class ProductMapper:Profile
    {
        public ProductMapper()
        {
            CreateMap<Products, ProductResponse>();
        }
    
    }
}
