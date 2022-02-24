using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Products.Create;
using MyElectricalShop.Application.ActionMethods.Products.GetProductList;
using MyElectricalShop.Application.ActionMethods.Products.GetProductById;
using MyElectricalShop.Domain.Models;


namespace MyElectricalShop.Application.ActionMethods.Products
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, CreatedProductResponse>();
            CreateMap<Category, CategoryResponseForProduct>();
            CreateMap<Company, CompanyResponseForProduct>();
            CreateMap<VoltageLevel, VoltageLevelResponseForProduct>();
            CreateMap<GetProductByIdRequest, ProductResponse>();
        }
    }
}
